import { getLocalStorageItem } from "../components/utilities.js";
import { validations } from "../components/validations.js";

let bothFieldsFilled = false;

function handleValidation(field, isValid, message) {
    let errorElement = field.nextElementSibling;

    if (!isValid) {
        if (!errorElement || !errorElement.classList.contains('error-message')) {
            errorElement = document.createElement('span');
            errorElement.className = 'error-message';
            field.parentNode.insertBefore(errorElement, field.nextSibling);
        }
        errorElement.textContent = message;
        field.style.borderColor = 'red';
    } else {
        if (errorElement && errorElement.classList.contains('error-message')) {
            errorElement.remove();
        }
        field.style.borderColor = 'green';
    }
}

export function validateInteractionForm() {
    let isValidForm = true;

    const fields = document.querySelectorAll('#add-interaction-form input, #add-interaction-form select');
    fields.forEach(field => {
        const validationKey = field.id;

        if (validations[validationKey]) {
            const { validate, errorMessage } = validations[validationKey];
            const isValid = validate(field.value);

            handleValidation(field, isValid, errorMessage);
            
            if (!isValid) {
                isValidForm = false;
            }
        }
    });

    return isValidForm;  
}

export function validateTaskForm() {
    let isValidForm = true;

    const fields = document.querySelectorAll('#task-form input, #task-form select');
    fields.forEach(field => {
        const validationKey = field.id;

        if (validations[validationKey]) {
            const { validate, errorMessage } = validations[validationKey];
            const isValid = validate(field.value);

            handleValidation(field, isValid, errorMessage);  

            if (!isValid) {
                isValidForm = false;
            }
        }
    });

    return isValidForm;
}

//Metodos para validar en tiempo real

export function validateFormField(event) {

    const field = event.target;

    const validationKey = field.dataset.validation; 

    if (validations[validationKey]) {

        const { validate, errorMessage } = validations[validationKey];

        const isValid = validate(field.value); 

        handleValidation(field, isValid, errorMessage);
    }
}

export function validateCreateProjectFormField(event) {
    const field = event.target;
    const validationKey = field.dataset.validation;
    
    let isValid = false;
    if (validations[validationKey]) {
        const { validate, errorMessage } = validations[validationKey];
        isValid = validate(field.value);
        handleValidation(field, isValid, errorMessage);
    }

    if (field.id === "start-datetime" || field.id === "end-datetime") {
        const otherFieldId = field.id === "start-datetime" ? "end-datetime" : "start-datetime";
        if (!isValid && bothFieldsFilled){
            bothFieldsFilled = false;
            validateOtherField(otherFieldId);
        }

        validateDateRange();
    }

    if (field.id === "project-name" && isValid){
        validateNameField(field.value);
    }
}

function validateDateRange() {
    const startDateTimeField = document.getElementById("start-datetime");
    const endDateTimeField = document.getElementById("end-datetime");

    const startDate = new Date(startDateTimeField.value);
    const endDate = new Date(endDateTimeField.value);

    if (!isNaN(startDate.getTime()) && !isNaN(endDate.getTime())) {

        bothFieldsFilled = true;

        if (endDate <= startDate) {
            handleValidation(endDateTimeField, false, "La fecha de fin debe ser posterior a la fecha de inicio.");
            handleValidation(startDateTimeField, false, "La fecha de inicio debe ser anterior a la fecha de fin.");
        } else {
            handleValidation(endDateTimeField, true, "");
            handleValidation(startDateTimeField, true, "");
        }
    }
}

function validateOtherField(otherFieldId) {
    const otherField = document.getElementById(otherFieldId);

    const validationKey = otherField.dataset.validation; 

    if (validations[validationKey]) {
        const { validate, errorMessage } = validations[validationKey];
        const isValid = validate(otherField.value);
        handleValidation(otherField, isValid, errorMessage);
    }
}

function validateNameField(value) {

    const projectNames = getLocalStorageItem('projectNames');
    const projectNameField = document.getElementById("project-name");

    if (projectNames && projectNames.some(name => name.trim().toLowerCase() === value.trim().toLowerCase())) {
        handleValidation(projectNameField, false, "Este nombre de proyecto ya está registrado.");
    }
    
    if (value.length > 255) {
        handleValidation(projectNameField, false, "El nombre del proyecto no puede exceder los 255 caracteres.");
    }
}

//Validar Project Form una cuando se realiza un submit

export function validateProjectForm() {
    const createProjectForm = document.getElementById('create-project-form');
    const inputs = createProjectForm.querySelectorAll('input, select');
    let isValid = true; 

    inputs.forEach(input => {
        const event = { target: input }; 
        validateCreateProjectFormField(event); 

        if (input.style.borderColor === 'red') {
            isValid = false; 
        }
    });
    return isValid; 
}

export function resetFormValidation(formSelector) {
    const form = document.querySelector(formSelector);
    const fields = form.querySelectorAll('input, select');  
    
    fields.forEach(field => {

        const errorElement = field.nextElementSibling;
        if (errorElement && errorElement.classList.contains('error-message')) {
            errorElement.remove();
        }

        field.style.borderColor = ''; 
    });

    bothFieldsFilled = false;
}
