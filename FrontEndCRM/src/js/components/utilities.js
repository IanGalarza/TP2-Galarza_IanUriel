export const setLocalStorageItem = (key, value) => {
    localStorage.setItem(key, JSON.stringify(value));
};

export const getLocalStorageItem = (key) => {
    const value = localStorage.getItem(key);
    return value ? JSON.parse(value) : null;
};

export const removeLocalStorageItem = (key) => {
    localStorage.removeItem(key);
};

export const clearLocalStorage = () => {
    localStorage.clear();
};

//URLS DEL BACKEND

const BASE_URL = 'https://localhost:7137/api/v1';

export const API_URLS = {
    CREATE_INTERACTION: (id) => `${BASE_URL}/Project/${id}/interactions`,
    CREATE_PROJECT: `${BASE_URL}/Project`,
    CREATE_TASK: (id) => `${BASE_URL}/Project/${id}/tasks`,
    GET_CAMPAIGN_TYPES: `${BASE_URL}/CampaignType`,
    GET_CLIENTS: `${BASE_URL}/Client`,
    GET_INTERACTION_TYPES: `${BASE_URL}/InteractionTypes`,
    GET_PROJECT_BY_ID: (id) => `${BASE_URL}/Project/${id}`,
    GET_PROJECTS: `${BASE_URL}/Project`,
    GET_TASK_STATUS: `${BASE_URL}/TaskStatus`,
    GET_USERS: `${BASE_URL}/User`,
    UPDATE_TASK: (id) => `${BASE_URL}/Tasks/${id}`,
};

export function updateProjectNames(newProjectName) {

    const storedProjectNames = getLocalStorageItem('projectNames') || [];

    storedProjectNames.push(newProjectName);

    setLocalStorageItem('projectNames', storedProjectNames);
}

export function showSpinner(){
    const spinnerContainer = document.querySelector('.spinner-container');
    const overlay = document.querySelector('.spinner-overlay');
    overlay.classList.add('active');
    spinnerContainer.style.display = "block";
}

export function hideSpinner() {
    const spinnerContainer = document.querySelector('.spinner-container');
    const overlay = document.querySelector('.spinner-overlay');
    overlay.classList.remove('active');
    spinnerContainer.style.display = "none";
}

export function sortProjectsByName(projects) {
    return projects.sort((a, b) => {
        if (a.name.toLowerCase() < b.name.toLowerCase()) {
            return -1;
        }
        if (a.name.toLowerCase() > b.name.toLowerCase()) {
            return 1;
        }
        return 0;
    });
}
