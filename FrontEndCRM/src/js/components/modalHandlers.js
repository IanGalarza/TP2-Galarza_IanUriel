import { renderProjectDetails, renderTaskForm, renderInteractionForm } from '../components/modalRenders.js';
import GetData from '../services/getData.js';
import { API_URLS, showSpinner, hideSpinner } from '../components/utilities.js';
import { openDetailsModal, openGenericFormModal } from '../components/modalManager.js';
import { modalUpdateTaskForm, modalAddInteractionForm, modalAddTaskForm } from '../components/modalFormManager.js';
import { loadAddTaskData, loadInteractionTypes } from '../components/dataLoader.js';
import { showErrorAlert } from '../components/alert.js';

export async function openProjectDetailsModal(projectId) {

    try {
        showSpinner();

        const projectData = await GetData.Get(API_URLS.GET_PROJECT_BY_ID(projectId)); 
    
        hideSpinner();
    
        if (projectData) {
            renderProjectDetails(projectData); 
            resetScrollDetailsPosition();
            openDetailsModal();
            configureProjectDetailsActions();
        }
        else{
            showErrorAlert('Se ha producido un error al querer acceder al proyecto.');
        }
    }
    catch (error){
        console.error('Error al acceder al proyecto:', error);
        hideSpinner();
    }
}

export async function handleAddInteraction(button, isModal){
    const projectId = button.dataset.projectId;
    renderInteractionForm();
    await loadInteractionTypes('interaction-type');
    openGenericFormModal();
    modalAddInteractionForm(projectId, isModal);
}

export async function handleAddTask(button, isModal){
    const projectId = button.dataset.projectId;
    renderTaskForm(false);
    await loadAddTaskData();
    openGenericFormModal();
    modalAddTaskForm(projectId, isModal);
}

export async function handleEditTask(button){
    const taskId = button.dataset.taskId;
    const taskName = button.dataset.taskName;
    const taskDueDate = button.dataset.taskDueDate; 
    const userAssignedId = button.dataset.taskAssignedTo;
    const taskStatusId = button.dataset.taskStatus;
    renderTaskForm(true);
    await loadAddTaskData();
    openGenericFormModal();
    configurateUpdateInitial(taskName, taskDueDate, userAssignedId, taskStatusId);
    modalUpdateTaskForm(taskId);
}

function configureProjectDetailsActions() {
    const editTaskButtons = document.querySelectorAll('.edit-task-btn'); 

    editTaskButtons.forEach(button => {
        button.addEventListener('click', (event) => {
            handleEditTask(event.target);       
        });
    });
}

function resetScrollDetailsPosition(){
    const firstScroll = document.querySelector('.interactions-list');
    const secondScroll = document.querySelector('.tasks-list');
    const ThirdScroll = document.querySelector('.modal-body-container');
    
    setTimeout(() => {
        firstScroll.scrollTop = 0;
        secondScroll.scrollTop = 0;
        ThirdScroll.scrollTop = 0;
    }, 0);
}

function configurateUpdateInitial(taskName, taskDueDate, userAssignedId, taskStatusId){
    document.getElementById('task-name').value = taskName;
    document.getElementById('task-due-date').value = taskDueDate;
    document.getElementById('user-select').value = userAssignedId;
    document.getElementById('status-select').value = taskStatusId;
}

export function assignEditTaskEvent(editButton) {
    editButton.addEventListener('click', (event) => {
        handleEditTask(event.target); 
    });
}