export default function ProjectRow(project) {
    const clientName = project.client.name;
    const campaignTypeName = project.campaignType.name;

    return `
    <tr>
        <td>${project.id}</td>
        <td>${project.name}</td>
        <td>${clientName}</td>
        <td>${campaignTypeName}</td>
        <td>${new Date(project.start).toLocaleDateString()}</td>
        <td>${new Date(project.end).toLocaleDateString()}</td>
        <td>
            <span class="view-details" data-project-id="${project.id}" title="Ver Detalles">
                <img src="./src/img/icono_detalles.png" alt="Ver Detalles" class="action-icon">
            </span>
            <span class="add-task" data-project-id="${project.id}" title="Agregar Tarea">
                <img src="./src/img/icono_tarea.png" alt="Agregar Tarea" class="action-icon">
            </span>
            <span class="add-interaction" data-project-id="${project.id}" title="Agregar Interacción">
                <img src="./src/img/icono_interaccion.png" alt="Agregar Interacción" class="action-icon">
            </span>
        </td>
    </tr>
    `;
}
