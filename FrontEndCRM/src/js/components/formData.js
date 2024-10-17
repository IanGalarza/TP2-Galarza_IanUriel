import { validateInteractionForm, validateProjectForm, validateTaskForm } from "../components/ValidationHandler.js";

export function getInteractionFormData() {
    const notes = document.getElementById('interaction-notes').value.trim();
    const interactionDate = document.getElementById('interaction-date').value;
    const interactionTypeId = document.getElementById('interaction-type').value;

    const isValid = validateInteractionForm();  
    if (!isValid) {
        return null;  
    }

    return {
        notes: notes,
        date: interactionDate,
        interactionType: parseInt(interactionTypeId)
    };
}

export function getProjectFormData() {
    const projectName = document.getElementById('project-name').value.trim();
    const clientId = document.getElementById('client-select-create').value;
    const campaignTypeId = document.getElementById('campaign-type-select-create').value;
    const startDateTime = document.getElementById('start-datetime').value;
    const endDateTime = document.getElementById('end-datetime').value;

    const isValid = validateProjectForm();
    if (!isValid){
        return null;
    }

    return {
        name: projectName,
        start: startDateTime,
        end: endDateTime,
        client: parseInt(clientId),        
        campaignType: parseInt(campaignTypeId)
    };
}

export function getTaskFormData() {
    const taskName = document.getElementById('task-name').value.trim();
    const dueDate = document.getElementById('task-due-date').value;
    const userId = document.getElementById('user-select').value;
    const statusId = document.getElementById('status-select').value;

    const isValid = validateTaskForm();  
    if (!isValid) {
        return null;  
    }

    return {
        name: taskName,
        dueDate: dueDate,
        user: parseInt(userId), 
        status: parseInt(statusId) 
    };
}
