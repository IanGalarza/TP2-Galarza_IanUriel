import GetData  from '../services/getData.js';
import { setLocalStorageItem, getLocalStorageItem } from '../components/utilities.js';
import { API_URLS } from '../components/utilities.js';
import { renderGenericSelect, renderGenericFilterSelect } from '../components/selectRenders.js';


export async function loadAddTaskData() {
    await loadUsers('user-select');
    await loadStatuses('status-select');
}

export async function loadFilterData() {
    document.getElementById('search-input').value = '';
    await loadClients('client-select', 'Filtrar cliente...', true, 'Todos los clientes.');
    await loadCampaignTypes('campaign-type-select', 'Filtrar campaña...', true, 'Todas las campañas.');
}

export async function loadCreateData() {
    await loadClients('client-select-create', 'Selecciona un cliente...', false);
    await loadCampaignTypes('campaign-type-select-create', 'Selecciona un tipo de campaña...', false);
}

export async function loadInteractionTypes(selectId) {
    const interactionTypes = await GetData.Get(API_URLS.GET_INTERACTION_TYPES); 

    renderGenericSelect(interactionTypes, selectId, 'Seleccionar tipo de interacción...'); 
}

async function loadUsers(selectId) {
    const users = await GetData.Get(API_URLS.GET_USERS);

    renderGenericSelect(users, selectId, 'Seleccionar usuario...');
}

async function loadStatuses(selectId) {
    const statuses = await GetData.Get(API_URLS.GET_TASK_STATUS);

    renderGenericSelect(statuses, selectId, 'Seleccionar estado...');
}

async function loadClients(selectId, defaultText, includeAllClientsOption = false, allOptionText = 'Todas las opciones.') {
    const clients = await GetData.Get(API_URLS.GET_CLIENTS);

    renderGenericFilterSelect(clients, selectId, defaultText, includeAllClientsOption, allOptionText);
}

async function loadCampaignTypes(selectId, defaultText, includeAllCampaignTypesOption = false, AllOptionText = 'Todas las opciones.') {
    const campaignTypes = await GetData.Get(API_URLS.GET_CAMPAIGN_TYPES);

    renderGenericFilterSelect(campaignTypes, selectId, defaultText, includeAllCampaignTypesOption, AllOptionText);
}

