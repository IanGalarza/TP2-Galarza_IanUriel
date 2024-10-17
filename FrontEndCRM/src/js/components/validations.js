export const validations = {
    'interaction-notes': {
        validate: (value) => value.trim() !== '', 
        errorMessage: 'Las notas no pueden estar vacías.'
    },
    'interaction-date': {
        validate: (value) => {
            if (!value) return false;  
            const datePattern = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/;
            if (!datePattern.test(value)) return false;
            const date = new Date(value);
            return !isNaN(date.getTime());  
        },  
        errorMessage: 'Debe seleccionar una fecha y hora válidas para la interacción.'
    },
    'interaction-type': {
        validate: (value) => value !== '',  
        errorMessage: 'Debe seleccionar un tipo de interacción.'
    },
    'task-name': {
        validate: (value) => value.trim() !== '',  
        errorMessage: 'El nombre de la tarea no puede estar vacío.'
    },
    'task-due-date': {
        validate: (value) => {
            if (!value) return false;  
            const datePattern = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/;
            if (!datePattern.test(value)) return false;
            const date = new Date(value);
            return !isNaN(date.getTime());  
        },  
        errorMessage: 'Debe seleccionar una fecha y hora válidas para la tarea.'
    },
    'user-select': {
        validate: (value) => value !== '',  
        errorMessage: 'Debe seleccionar un usuario.'
    },
    'status-select': {
        validate: (value) => value !== '', 
        errorMessage: 'Debe seleccionar un estado para la tarea.'
    },
    'project-name': {
        validate: (value) => {
            return value.trim() !== ""; 
        },
        errorMessage: "El nombre del proyecto no puede estar vacío."
    },
    'client-select-create': {
        validate: (value) => {
            return value !== "";  
        },
        errorMessage: "El cliente debe ser seleccionado."
    },
    'campaign-type-select-create': {
        validate: (value) => {
            return value !== "";  
        },
        errorMessage: "El tipo de campaña debe ser seleccionado."
    },
    'start-datetime': {
        validate: (value) => {
            if (!value) return false;
            const datePattern = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/; 
            if (!datePattern.test(value)) return false; 
            const date = new Date(value);
            return !isNaN(date.getTime());
        },
        errorMessage: "La fecha y hora de inicio no es válida."
    },
    'end-datetime': {
        validate: (value) => {
            if (!value) return false;  
            const datePattern = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/;  
            if (!datePattern.test(value)) return false;  
            const date = new Date(value);
            return !isNaN(date.getTime());
        },
        errorMessage: "La fecha y hora de fin no es válida."
    }
};
