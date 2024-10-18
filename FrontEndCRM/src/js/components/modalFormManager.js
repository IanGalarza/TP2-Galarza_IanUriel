import { getTaskFormData, getInteractionFormData } from '../components/formData.js';
import { API_URLS, showSpinner, hideSpinner } from '../components/utilities.js';
import { closeGenericFormModal} from '../components/modalManager.js';
import { sucessfulUpdateTask, sucessfulAddInteraction, sucessfulAddTask} from '../components/sucessfulResponseHandlers.js';
import PutData from '../services/putData.js';
import PatchData from '../services/patchData.js';
import { validateFormField } from '../components/ValidationHandler.js';
import { showAlert, showErrorAlert } from '../components/alert.js';

export async function modalUpdateTaskForm(taskId) {
    const updateTaskButton = document.querySelector('.update-task-button');
    const updateTaskForm = document.getElementById('task-form');
    const inputs = updateTaskForm.querySelectorAll('input, select');

    inputs.forEach(input => {
        input.addEventListener('input', validateFormField); 
        input.addEventListener('change', validateFormField); 
        input.addEventListener('blur', validateFormField);
    });

    updateTaskButton.addEventListener('click', async () => {
        const requestData = getTaskFormData();  
        if (!requestData) {
            showErrorAlert('Por favor, revise todos los campos.');
            return;
        }
        try {
            showSpinner();

            const response = await PutData.Put(API_URLS.UPDATE_TASK(taskId), requestData);  
            
            hideSpinner();

            if (response) {
                showAlert('Tarea actualizada exitosamente.');
                document.getElementById('task-form').reset();  
                sucessfulUpdateTask(response);
            }
            else{
                showErrorAlert('Error al actualizar la tarea. Por favor, inténtelo nuevamente.');
            }
        } catch (error) {
            showErrorAlert('Se ha producido un error inesperado.');
            console.error('Error al actualizar la tarea:', error);
            hideSpinner();
        }
    });
}

//Add-Interactions

export async function modalAddInteractionForm(projectId, isModal) {
    const saveInteractionButton = document.querySelector('.save-interaction-button'); 
    
    const createInteractionForm = document.getElementById('add-interaction-form');
    const inputs = createInteractionForm.querySelectorAll('input, select');

    inputs.forEach(input => {
        input.addEventListener('input', validateFormField); 
        input.addEventListener('change', validateFormField); 
        input.addEventListener('blur', validateFormField);
    });
    
    saveInteractionButton.addEventListener('click', async () => {
        const requestData = getInteractionFormData(); 
        if (!requestData) {
            showErrorAlert('Por favor, revise todos los campos.');
            return;
        }
        try {
            showSpinner();

            const response = await PatchData.Patch(API_URLS.CREATE_INTERACTION(projectId), requestData); 

            hideSpinner();

            if (response) {
                showAlert('Interacción agregada exitosamente.');
                document.getElementById('add-interaction-form').reset(); 
                if (isModal) {
                    sucessfulAddInteraction(response);
                }
                else {
                    closeGenericFormModal()
                }
            }
            else {
                showErrorAlert('Error al agregar la interacción. Por favor, inténtelo nuevamente.');
            }
        } catch (error) {
            showErrorAlert('Se ha producido un error inesperado.');
            console.error('Error al agregar la interacción:', error);
            hideSpinner();
        }
    });
}

//Add-Tasks

export async function modalAddTaskForm(projectId, isModal) {

    const saveTaskButton = document.querySelector('.save-task-button'); 
    const createTaskForm = document.getElementById('task-form');
    const inputs = createTaskForm.querySelectorAll('input, select');

    inputs.forEach(input => {
        input.addEventListener('input', validateFormField); 
        input.addEventListener('change', validateFormField); 
        input.addEventListener('blur', validateFormField);
    });

    saveTaskButton.addEventListener('click', async () => {
        const requestData = getTaskFormData(); 
        if (!requestData) {
            showErrorAlert('Por favor, revise todos los campos.');
            return; 
        }
        try {
            showSpinner();

            const response = await PatchData.Patch(API_URLS.CREATE_TASK(projectId), requestData);

            hideSpinner();

            if (response) {
                showAlert('Tarea agregada exitosamente.');
                document.getElementById('task-form').reset(); 
                if (isModal) {
                    sucessfulAddTask(response);
                }
                else {
                    closeGenericFormModal()
                }
            }
            else{
                showErrorAlert('Error al agregar la tarea. Por favor, inténtelo nuevamente.');
            }
        } catch (error) {
            showErrorAlert('Se ha producido un error inesperado.');
            console.error('Error al agregar la tarea:', error);
            hideSpinner();
        }
    }); 
}