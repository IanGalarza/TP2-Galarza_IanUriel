export function renderGenericSelect(items, selectId, defaultText) {
    const selectElement = document.getElementById(selectId);

    if (!selectElement) return;

    selectElement.innerHTML = '';

    const defaultOption = document.createElement('option');
    defaultOption.value = '';
    defaultOption.disabled = true;
    defaultOption.selected = true;
    defaultOption.textContent = defaultText;
    selectElement.appendChild(defaultOption);

    items.forEach(item => {
        const option = document.createElement('option');
        option.value = item.id;
        option.textContent = `${item.id}. ${item.name} ${item.email ? `(${item.email})` : ''}`; 
        selectElement.appendChild(option);
    });
}

export function renderGenericFilterSelect(items, selectId, defaultText, includeAllOption, allOptionText) {
    const selectElement = document.getElementById(selectId);

    if (!selectElement) return;

    selectElement.innerHTML = ''; 

    const defaultOption = document.createElement('option');
    defaultOption.value = '';
    defaultOption.disabled = true;
    defaultOption.selected = true;
    defaultOption.textContent = defaultText;
    selectElement.appendChild(defaultOption);

    items.forEach(item => {
        const option = document.createElement('option');
        option.value = item.id;
        option.textContent = `${item.id}. ${item.name}`; 
        selectElement.appendChild(option);
    });

    if (includeAllOption) {
        const allOption = document.createElement('option');
        allOption.value = '';
        allOption.textContent = allOptionText;
        selectElement.appendChild(allOption);
    }
}

