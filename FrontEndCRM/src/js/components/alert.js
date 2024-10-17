export function alert(text, Class, iconUrl) {
    return `
        <div class="${Class} alert">
            <div class="alert-icon">
                <img src="${iconUrl}" alt="Icon" class="alert-logo" />
            </div>
            <p class="alert-text">${text}</p>
        </div>
    `;
}

export function showAlert(text) {
    return new Promise((resolve) => {
        const prevAlert = document.querySelector('.alert');
        if (prevAlert) {
            return;
        }

        let alertContainer = document.querySelector('.inform-container');
        let data = alert(text, 'alert', './src/img/success_icon.png'); 
        alertContainer.innerHTML += data;

        setTimeout(() => {
            let alertElement = document.querySelector('.alert');
            if (alertElement) {
                alertElement.classList.add('remove');
                setTimeout(() => {
                    alertElement.remove();
                    resolve(); 
                }, 500); 
            }
        }, 1300);  
    }); 
}

export function showErrorAlert(text) {
    return new Promise((resolve) => {
        const prevAlert = document.querySelector('.alert');
        if (prevAlert) {
            return;
        }
        let alertContainer = document.querySelector('.inform-container');
        let data = alert(text, 'alert-error', './src/img/error_icon.png'); 
        alertContainer.innerHTML += data;

        setTimeout(() => {
            let alertElement = document.querySelector('.alert');
            if (alertElement) {
                alertElement.classList.add('remove');
                setTimeout(() => {
                    alertElement.remove();
                    resolve(); 
                }, 500); 
            }
        }, 1700); 
    });
}
