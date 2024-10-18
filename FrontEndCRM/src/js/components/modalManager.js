export function openDetailsModal() {
    const modal = document.getElementById('project-details-modal');
    const overlay = document.getElementById('modal-overlay');

    modal.classList.remove('hidden'); 
    modal.classList.add('active');
    overlay.classList.add('active');
}

export function closeDetailsModal() {
    const modal = document.getElementById('project-details-modal');
    const overlay = document.getElementById('modal-overlay');
    modal.classList.remove('active'); 
    modal.classList.add('hidden');   
    overlay.classList.remove('active');  
}

export function openGenericFormModal() {
    const modal = document.getElementById('generic-modal-form');
    const overlay = document.getElementById('form-overlay');


    modal.classList.remove('hidden'); 
    modal.classList.add('active');
    overlay.classList.add('active');

}

export function closeGenericFormModal() {
    const modal = document.getElementById('generic-modal-form');
    const overlay = document.getElementById('form-overlay');
    modal.classList.remove('active'); 
    modal.classList.add('hidden');   
    overlay.classList.remove('active');  
}
