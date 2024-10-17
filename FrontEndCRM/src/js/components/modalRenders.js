export function renderProjectDetails(projectData) {

    const projectInfo = document.querySelector('#project-details-modal .project-info');
    const clientInfo = document.querySelector('#project-details-modal .client-info');
    const interactionsList = document.querySelector('#project-details-modal .interactions-list');
    const tasksList = document.querySelector('#project-details-modal .tasks-list');
    const modalFooter = document.querySelector('#project-details-modal .modal-footer');
    
    projectInfo.innerHTML = '';
    clientInfo.innerHTML = '';
    interactionsList.innerHTML = '';
    tasksList.innerHTML = '';

    const project = projectData.data;
    projectInfo.innerHTML = `
        <p><strong>Id:</strong> ${project.id}</p>
        <p><strong>Nombre:</strong> ${project.name}</p>
        <p><strong>Inicio:</strong> ${new Date(project.start).toLocaleString()}</p>
        <p><strong>Fin:</strong> ${new Date(project.end).toLocaleString()}</p>
        <p><strong>Tipo de campaña:</strong> ${project.campaignType.name}</p>
    `;

    const client = project.client;
    clientInfo.innerHTML = `
        <p><strong>Id:</strong> ${client.id}</p>
        <p><strong>Cliente:</strong> ${client.name}</p>
        <p><strong>Email:</strong> ${client.email}</p>
        <p><strong>Compañía:</strong> ${client.company}</p>
        <p><strong>Teléfono:</strong> ${client.phone}</p>
        <p><strong>Dirección:</strong> ${client.address}</p>
    `;

    if (projectData.interactions && projectData.interactions.length > 0) {
        projectData.interactions.forEach(interaction => {
            const interactionItem = document.createElement('li');
            interactionItem.innerHTML = `
                <p><strong>Id:</strong> ${interaction.id}</p>
                <p><strong>Notas:</strong> ${interaction.notes}</p>
                <p><strong>Fecha:</strong> ${new Date(interaction.date).toLocaleString()}</p>
                <p><strong>Tipo de interacción:</strong> ${interaction.interactionType.name}</p>
            `;
            interactionsList.appendChild(interactionItem);
        });
    }
    else {
        const noInteractionsMsg = document.createElement('li');
        noInteractionsMsg.innerHTML = `<p>No hay interacciones disponibles.</p>`;
        interactionsList.appendChild(noInteractionsMsg);
    }

    if (projectData.tasks && projectData.tasks.length > 0) {
        projectData.tasks.forEach(task => {
            const taskItem = document.createElement('li');
            taskItem.setAttribute('data-task-id', task.id);
            taskItem.innerHTML = `
                <div class="task-header">
                    <p><strong>Id:</strong> ${task.id}</p>
                    <button class="edit-task-btn" 
                    data-task-id="${task.id}"
                    data-task-name="${task.name}"
                    data-task-due-date="${task.dueDate}"
                    data-task-assigned-to="${task.userAssigned.id}"
                    data-task-status="${task.status.id}">
                    Modificar</button>
                </div>
                <p><strong>Tarea:</strong> ${task.name}</p>
                <p><strong>Fecha de vencimiento:</strong> ${new Date(task.dueDate).toLocaleString()}</p>
                <p><strong>Estado:</strong> ${task.status.name}</p>
                <p><strong>Asignada a:</strong> ${task.userAssigned.name} (${task.userAssigned.email})</p>
            `;
            tasksList.appendChild(taskItem);
        });
    }
    else {
        const noTasksMsg = document.createElement('li');
        noTasksMsg.innerHTML = `<p>No hay tareas disponibles.</p>`;
        tasksList.appendChild(noTasksMsg);
    }

    const taskButton = modalFooter.querySelector('.add-task-button');
    taskButton.setAttribute('data-project-id', project.id);

    const interactionButton = modalFooter.querySelector('.add-interaction-button');
    interactionButton.setAttribute('data-project-id', project.id);
}

export function renderTaskForm(IsEdit) {
    const modalBody = document.querySelector('#generic-modal-form .modal-body');
    const modalFooter = document.querySelector('#generic-modal-form .modal-footer');
    const modalTitle = document.querySelector('#generic-modal-form .modal-title');

    modalBody.innerHTML = '';

    modalBody.innerHTML = `
        <form id="task-form" class="modal-form">
            <label for="task-name" class="form-label">Nombre de la tarea:</label>
            <input type="text" id="task-name" class="form-input" placeholder="Ingrese el nombre de la tarea" required data-validation="task-name" autocomplete="off"/>

            <label for="task-due-date" class="form-label">Fecha y Hora de Vencimiento:</label>
            <input type="datetime-local" id="task-due-date" name="task-due-date" class="form-input" required data-validation="task-due-date"/>

            <label for="user-select" class="form-label">Usuario:</label>
            <select id="user-select" name="user-select" class="form-select" required data-validation="user-select"></select>

            <label for="status-select" class="form-label">Estado:</label>
            <select id="status-select" name="status-select" class="form-select" required data-validation="status-select"></select>
        </form>
    `;
    if (IsEdit) {
        modalTitle.innerHTML = 'Editar Tarea';
        modalFooter.innerHTML = `
            <button class="update-task-button">Guardar Cambios</button>
        `; 
    }
    else {
        modalTitle.innerHTML = 'Agregar Tarea';
        modalFooter.innerHTML = `
        <button class="save-task-button">Crear Tarea</button>
        `;
    }
}

export function renderInteractionForm() {
    const modalTitle = document.querySelector('#generic-modal-form .modal-title');
    const modalBody = document.querySelector('#generic-modal-form .modal-body');
    const modalFooter = document.querySelector('#generic-modal-form .modal-footer');

    modalBody.innerHTML = '';

    modalBody.innerHTML = `
        <form id="add-interaction-form" class="modal-form">
            <label for="interaction-notes" class="form-label">Notas:</label>
            <input type="text" id="interaction-notes" class="form-input" placeholder="Escribe las notas de la interacción" required data-validation="interaction-notes" autocomplete="off"/>

            <label for="interaction-date" class="form-label">Fecha:</label>
            <input type="datetime-local" id="interaction-date" name="interaction-date" class="form-input" required data-validation="interaction-date"/>

            <label for="interaction-type" class="form-label">Tipo de Interacción:</label>
            <select id="interaction-type" name="interaction-type" class="form-select" required data-validation="interaction-type"></select>
        </form>
    `;
    modalTitle.innerHTML = 'Agregar Interaccion';
    modalFooter.innerHTML = `<button class="save-interaction-button">Crear Interacción</button>`;
}