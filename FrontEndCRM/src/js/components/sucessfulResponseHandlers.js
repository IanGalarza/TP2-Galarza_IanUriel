import { closeGenericFormModal, openDetailsModal } from "../components/modalManager.js";
import { assignEditTaskEvent } from "../components/modalHandlers.js";

export function sucessfulUpdateTask(response) {

    closeGenericFormModal();
    
    const updatedTask = response;

    const tasksList = document.querySelector('#project-details-modal .tasks-list');

    const taskItem = tasksList.querySelector(`li[data-task-id="${updatedTask.id}"]`);

    if (taskItem) {
        taskItem.innerHTML = `
            <div class="task-header">
                <p><strong>Id:</strong> ${updatedTask.id}</p>
                <button class="edit-task-btn" 
                data-task-id="${updatedTask.id}"
                data-task-name="${updatedTask.name}"
                data-task-due-date="${updatedTask.dueDate}"
                data-task-assigned-to="${updatedTask.userAssigned.id}"
                data-task-status="${updatedTask.status.id}">
                Modificar</button>
            </div>
            <p><strong>Tarea:</strong> ${updatedTask.name}</p>
            <p><strong>Fecha de vencimiento:</strong> ${new Date(updatedTask.dueDate).toLocaleString()}</p>
            <p><strong>Estado:</strong> ${updatedTask.status.name}</p>
            <p><strong>Asignada a:</strong> ${updatedTask.userAssigned.name} (${updatedTask.userAssigned.email})</p>
        `;

        const editButton = taskItem.querySelector('.edit-task-btn');

        assignEditTaskEvent(editButton);

        openDetailsModal();
    } else {
        console.error("No se encontró la tarea con el ID: " + updatedTask.id);
    }
}

export function sucessfulAddInteraction(response){
    closeGenericFormModal();
    const interactionsList = document.querySelector('#project-details-modal .interactions-list');

    if (interactionsList.children.length === 1 && interactionsList.children[0].textContent.includes("No hay interacciones disponibles")) {
        interactionsList.removeChild(interactionsList.children[0]); 
    }

    const interactionItem = document.createElement('li');
    interactionItem.setAttribute('data-interaction-id', response.id); 

    interactionItem.innerHTML = `
        <p><strong>Id:</strong> ${response.id}</p>
        <p><strong>Notas:</strong> ${response.notes}</p>
        <p><strong>Fecha:</strong> ${new Date(response.date).toLocaleString()}</p>
        <p><strong>Tipo de interacción:</strong> ${response.interactionType.name}</p>
    `;

    interactionsList.appendChild(interactionItem);

    openDetailsModal();
}

export function sucessfulAddTask(response){

    closeGenericFormModal();

    const tasksList = document.querySelector('#project-details-modal .tasks-list');

    if (tasksList.children.length === 1 && tasksList.children[0].textContent.includes("No hay tareas disponibles")) {
        tasksList.removeChild(tasksList.children[0]); 
    }

    const taskItem = document.createElement('li');
    taskItem.setAttribute('data-task-id', response.id); 

    taskItem.innerHTML = `
        <div class="task-header">
            <p><strong>Id:</strong> ${response.id}</p>
            <button class="edit-task-btn" 
            data-task-id="${response.id}"
            data-task-name="${response.name}"
            data-task-due-date="${response.dueDate}"
            data-task-assigned-to="${response.userAssigned.id}"
            data-task-status="${response.status.id}">
            Modificar</button>
        </div>
        <p><strong>Tarea:</strong> ${response.name}</p>
        <p><strong>Fecha de vencimiento:</strong> ${new Date(response.dueDate).toLocaleString()}</p>
        <p><strong>Estado:</strong> ${response.status.name}</p>
        <p><strong>Asignada a:</strong> ${response.userAssigned.name} (${response.userAssigned.email})</p>
    `;
    tasksList.appendChild(taskItem);

    const editButton = taskItem.querySelector('.edit-task-btn');

    assignEditTaskEvent(editButton);

    openDetailsModal();
}