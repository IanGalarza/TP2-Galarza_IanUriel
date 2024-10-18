import GetData from '../services/getData.js';
import PostData from '../services/postData.js';
import ProjectRow from '../components/projectRow.js'; 
import { showAlert, showErrorAlert } from '../components/alert.js';
import { resetFormValidation  } from "../components/ValidationHandler.js";

import { validateCreateProjectFormField } from '../components/ValidationHandler.js';
import { loadFilterData, loadCreateData} from '../components/dataLoader.js';
import { API_URLS , setLocalStorageItem, updateProjectNames, showSpinner, hideSpinner, sortProjectsByName, clearLocalStorage, removeLocalStorageItem} from '../components/utilities.js';
import { closeDetailsModal, closeGenericFormModal } from '../components/modalManager.js';
import { getProjectFormData} from '../components/formData.js';
import { handleAddInteraction, handleAddTask, openProjectDetailsModal } from '../components/modalHandlers.js';


document.addEventListener('DOMContentLoaded', () => initializeApp());

function initializeApp() {
    filterProjects();
    loadProjects();
    loadFilterData();
    configureViews();
    configureCreateProjectForm();
    configureModalActions();
}

//lista de proyectos

async function loadProjects() {

    try {
        showSpinner();

        let projectsData = await GetData.Get(API_URLS.GET_PROJECTS);

        const projectNames = projectsData.map(project => project.name);

        setLocalStorageItem('projectNames', projectNames);

        let sortedProjects = sortProjectsByName(projectsData);

        displayProjects(sortedProjects);

    } catch (error) {
        showErrorAlert('Ha ocurrido un error al cargar los proyectos.');
        console.error('Error al cargar los proyectos:', error);
        hideSpinner();
    }
    finally{
        hideSpinner();
    }
}

//filtros

function filterProjects() {
    const searchQuery = document.getElementById("search-input");
    const clientId = document.getElementById("client-select");
    const campaignTypeId = document.getElementById("campaign-type-select");

    searchQuery.addEventListener('input', () => handlerFilterProject(searchQuery, clientId, campaignTypeId));
    clientId.addEventListener('change', () => handlerFilterProject(searchQuery, clientId, campaignTypeId));
    campaignTypeId.addEventListener('change', () => handlerFilterProject(searchQuery, clientId, campaignTypeId));
}

async function handlerFilterProject(name, client, campaignType) {
    try{
    const searchQuery = name.value.trim();
    const clientId = client.value;
    const campaignTypeId = campaignType.value;
    
    const url = new URL(API_URLS.GET_PROJECTS);
    
    const params = new URLSearchParams();

    if (searchQuery) {
        params.append('name', searchQuery);
    }
    if (clientId) {
        params.append('client', clientId);
    }
    if (campaignTypeId) {
        params.append('campaign', campaignTypeId);
    }

    url.search = params.toString();

    let projectsData = await GetData.Get(url.toString());

    let sortedProjects = sortProjectsByName(projectsData);

    displayProjects(sortedProjects);
    }
    catch{
        showErrorAlert('Ha ocurrido un error al cargar los proyectos filtrados.');
        console.error('Error al cargar los proyectos:', error);
    }
}

//Configuracion para Alternar vistas entre ver proyectos

function configureViews() {
    const viewProjectsBtn = document.getElementById('view-projects-btn');
    const createProjectBtn = document.getElementById('create-project-btn');
    const projectView = document.getElementById('project-view');
    const createProjectView = document.getElementById('create-project-view');
    const underline = document.getElementById('underline');

    viewProjectsBtn.disabled = true;
    createProjectBtn.disabled = false;

    updateUnderLine(viewProjectsBtn, underline);

    viewProjectsBtn.addEventListener('click', () => {
        if (!viewProjectsBtn.disabled){
            toggleViews(projectView, createProjectView);
            updateUnderLine(viewProjectsBtn, underline);
            resetProjectView();
            viewProjectsBtn.disabled = true;
            createProjectBtn.disabled = false;
        }
    });
    createProjectBtn.addEventListener('click', () => {
        if (!createProjectBtn.disabled){
            toggleViews(createProjectView, projectView);
            updateUnderLine(createProjectBtn, underline);
            resetCreateProjectView();
            loadCreateData();
            createProjectBtn.disabled = true;
            viewProjectsBtn.disabled = false;
        }
    });
}

function toggleViews(showView, hideView) {
    showView.classList.remove('hidden-view');
    hideView.classList.add('hidden-view');
}

function updateUnderLine(button, underline){
    const left = button.offsetLeft;
    const width = button.offsetWidth;
    
    underline.style.left = `${left}px`;
    underline.style.width = `${width}px`;
}

function resetProjectView() {
    document.getElementById('search-input').value = '';
    const clientSelect = document.getElementById('client-select');
    const campaignTypeSelect = document.getElementById('campaign-type-select');
    clientSelect.selectedIndex = 0; 
    campaignTypeSelect.selectedIndex = 0; 
    loadProjects();
    resetCreateProjectView();
}

function resetCreateProjectView() {
    const createProjectForm = document.getElementById('create-project-form');
    createProjectForm.reset(); 
    resetFormValidation('#create-project-form');
}

//Displays

function displayProjects(projectsData) {
    const tableBody = document.querySelector('#project-table-body');

    if (!projectsData || projectsData.length === 0) {
        tableBody.innerHTML = '<tr><td colspan="7">No hay proyectos disponibles actualmente.</td></tr>';
        return;
    }

    tableBody.innerHTML = '';

    projectsData.forEach(project => {
        const projectRowHtml = ProjectRow(project);
        tableBody.innerHTML += projectRowHtml;
    });

    configureModalActions(); 
}

//Crear Proyecto

function configureCreateProjectForm() {
    const createProjectForm = document.getElementById('create-project-form');
    const inputs = createProjectForm.querySelectorAll('input, select');


    inputs.forEach(input => {
        input.addEventListener('input', validateCreateProjectFormField); 
        input.addEventListener('change', validateCreateProjectFormField); 
        input.addEventListener('blur', validateCreateProjectFormField);
    });

    createProjectForm.addEventListener('submit', async (event) => {
        
        event.preventDefault(); 

        const requestData = getProjectFormData();

        if (!requestData) {
            showErrorAlert('Por favor, revise todos los campos.');
            return;
        }

        try {
            showSpinner();

            const response = await PostData.Post(API_URLS.CREATE_PROJECT,requestData);

            hideSpinner();

            if (response) {
                showAlert('Proyecto creado exitosamente.');

                resetFormValidation('#create-project-form');

                createProjectForm.reset();

                const selects = createProjectForm.querySelectorAll('select');
                selects.forEach(select => {
                    select.selectedIndex = 0; 
                });
                
                updateProjectNames(requestData.name.trim());
            }
            else{
                showErrorAlert('Error al crear el proyecto. Por favor, inténtelo nuevamente.')
            }
        } catch (error) {
            showErrorAlert('Se ha producido un error inesperado.');
            console.error('Error al crear el proyecto:', error);
            hideSpinner();
        }
    });
}

//Eventos

function configureModalActions() {
    const viewDetailsButtons = document.querySelectorAll('.view-details');
    const addTableTaskButtons = document.querySelectorAll('.add-task');
    const addTableInteractionButtons = document.querySelectorAll('.add-interaction');

    viewDetailsButtons.forEach(button => {
        button.addEventListener('click', () => {
            const projectId = button.dataset.projectId;
            openProjectDetailsModal(projectId); 
        });
    });

    addTableTaskButtons.forEach(button => {
        button.addEventListener('click', () => handleAddTask(button, false));
    });

    addTableInteractionButtons.forEach(button => {
        button.addEventListener('click', () => handleAddInteraction(button, false));
    });

    document.addEventListener('click', (event) => {
        const closeBtn = event.target.closest('.close-details-button');
        if (closeBtn) {
            closeDetailsModal();
        }
    });

    document.addEventListener('click', (event) => {
        const overlay = document.getElementById('modal-overlay');
        const modal = document.querySelector('.modal-details.active'); 
        if (modal && overlay && event.target === overlay) {
            closeDetailsModal();
        }
    });

    document.addEventListener('click', (event) => {
        const closeBtn = event.target.closest('.close-form-button');
        if (closeBtn) {
            closeGenericFormModal();
        }
    });

    document.addEventListener('click', (event) => {
        const overlay = document.getElementById('form-overlay');
        const modal = document.querySelector('.generic-modal-form.active'); 
        if (modal && overlay && event.target === overlay) {
            closeGenericFormModal();
        }
    });
}
