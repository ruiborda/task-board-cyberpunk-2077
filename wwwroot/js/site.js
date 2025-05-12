import { TaskService } from './TaskService.js';

console.log("Hello World desde JS en Razor Pages");

window.testTaskService = async function() {
  try {
    const testSample = {
      title: 'Fix bug #42 in TaskController',
      description: 'Resolve null-reference error when updating task status via API.',
      dueDate: '2025-05-12T00:00:00Z',
      statusId: 'e1f7b815-e9eb-48c1-a487-d90097a5b03f',
      createdBy: '582c1bd5-c2fb-42eb-ba97-300956fae069',
    };
    
    console.log('--- Ejecutando pruebas de TaskService ---');

    console.log('Llamada a TaskService.createTask');
    const created = await TaskService.createTask(testSample);
    console.log('Resultado createTask:', created);
    
    console.log('Llamada a TaskService.getAllTasks()');
    const all = await TaskService.getAllTasks();
    console.log('Resultado getAllTasks:', all);

    console.log(`Llamada a TaskService.getTaskById(${created.id})`);
    const single = await TaskService.getTaskById(created.id);
    console.log('Resultado getTaskById:', single);

  
    console.log('Llamada a TaskService.updateTask');
    const updated = await TaskService.updateTask({ ...testSample, id: created.id });
    console.log('Resultado updateTask:', updated);

    console.log('Llamada a TaskService.updateTaskStatus');
    const statusUpdated = await TaskService.updateTaskStatus({ id: created.id, statusId: updated.statusId });
    console.log('Resultado updateTaskStatus:', statusUpdated);

    console.log('Llamada a TaskService.deleteTask');
    const deletion = await TaskService.deleteTask(created.id);
    console.log('Resultado deleteTask:', deletion);

    console.log('--- Pruebas completadas ---');
  } catch (err) {
    console.error('Error en testTaskService:', err);
  }
};

// Task Board Implementation
function renderTaskBoard() {
  const statusList = TaskService.getAllStatuses();
  statusList.forEach(status => {
    // Use full status ID rather than substring
    const tasksContainer = document.getElementById(`tasks-${status.id}`);
    if (tasksContainer) tasksContainer.innerHTML = "";
  });
  
  statusList.forEach(status => renderTasksInColumn(status.id));
}

function renderTasksInColumn(statusId) {
  // Use full status ID without substring
  const tasksContainer = document.getElementById(`tasks-${statusId}`);
  if (!tasksContainer) return;
  
  TaskService.getAllTasks().then(tasks => {
    const filteredTasks = tasks.filter(t => t.statusId === statusId);
    tasksContainer.innerHTML = "";
    
    filteredTasks.forEach(task => {
      const cardElement = renderTaskCard(task);
      tasksContainer.appendChild(cardElement);
    });

    setupCardInteractions(tasksContainer);
  }).catch(err => console.error("Error loading tasks:", err));
}

function setupCardInteractions(container) {
  // Setup actions buttons
  container.querySelectorAll(".card-actions-button").forEach(button => {
    button.addEventListener("click", e => {
      e.stopPropagation();
      const taskId = e.currentTarget.dataset.id;
      const popupElement = document.getElementById(`popup-${taskId}`);
      const wasVisible = popupElement.classList.contains("card-actions-popup-visible");
      
      // Hide all popups first
      document.querySelectorAll(".card-actions-popup")
        .forEach(p => p.classList.remove("card-actions-popup-visible"));
      
      if (!wasVisible) {
        popupElement.classList.add("card-actions-popup-visible");
        const rect = e.currentTarget.getBoundingClientRect();
        popupElement.style.position = "fixed";
        popupElement.style.top = `${rect.bottom + 4}px`;
        
        // Adjust popup position to stay within viewport
        const popupWidth = popupElement.offsetWidth;
        const buttonCenter = rect.left + rect.width / 2;
        let popupLeft = buttonCenter - popupWidth / 2;
        if (popupLeft < 0) popupLeft = 4; 
        if (popupLeft + popupWidth > window.innerWidth) popupLeft = window.innerWidth - popupWidth - 4;

        popupElement.style.left = `${popupLeft}px`;
      }
    });
    
    button.addEventListener("mousedown", e => e.stopPropagation());
    button.addEventListener("dragstart", e => e.stopPropagation());
  });

  // Setup popup interactions
  container.querySelectorAll(".card-actions-popup").forEach(popup => {
    popup.addEventListener("mousedown", e => e.stopPropagation());
    popup.addEventListener("dragstart", e => e.stopPropagation());
    
    popup.querySelectorAll("button").forEach(innerBtn => {
      innerBtn.addEventListener("mousedown", e => e.stopPropagation());
      innerBtn.addEventListener("dragstart", e => e.stopPropagation());
    });
  });

  // Setup edit buttons
  container.querySelectorAll(".card-actions-popup-edit").forEach(editBtn => {
    editBtn.addEventListener("click", e => {
      e.stopPropagation();
      const taskId = e.currentTarget.dataset.id;
      TaskService.getTaskById(taskId).then(task => {
        if (task) showTaskForm(task);
      }).catch(err => console.error("Error loading task for edit:", err));
    });
  });

  // Setup delete buttons
  container.querySelectorAll(".card-actions-popup-delete").forEach(deleteBtn => {
    deleteBtn.addEventListener("click", e => {
      e.stopPropagation();
      const taskId = e.currentTarget.dataset.id;
      showConfirmDialog(taskId);
    });
  });

  // Setup status change buttons
  container.querySelectorAll(".card-actions-popup-status").forEach(statusBtn => {
    statusBtn.addEventListener("click", e => {
      e.stopPropagation();
      const el = e.currentTarget;
      const taskId = el.dataset.id;
      const newStatusId = el.dataset.statusId;
      
      TaskService.updateTaskStatus({ id: taskId, statusId: newStatusId })
        .then(() => renderTaskBoard())
        .catch(err => console.error("Error updating task status:", err));
    });
  });

  // Setup drag and drop for cards
  container.querySelectorAll(".card").forEach(card => {
    card.setAttribute("draggable", "true");
    
    card.addEventListener("dragstart", e => {
      if (e.dataTransfer) {
        e.dataTransfer.setData("text/plain", card.getAttribute("data-id"));
      }
      setTimeout(() => card.classList.add("dragging"), 0);
    });
    
    card.addEventListener("dragend", () => card.classList.remove("dragging"));
    
    card.addEventListener("click", e => {
      e.stopPropagation();
      const taskId = card.getAttribute("data-id");
      TaskService.getTaskById(taskId).then(task => {
        if (task) showTaskForm(task);
      }).catch(err => console.error("Error loading task details:", err));
    });
  });
}

function renderTaskCard(task) {
  const cardElement = document.createElement("div");
  cardElement.className = "card";
  cardElement.dataset.id = task.id;

  // Title row with actions button
  const titleRow = document.createElement("div");
  titleRow.className = "card-title-row";
  
  const titleSpan = document.createElement("span");
  titleSpan.className = "card-title";
  titleSpan.textContent = task.title;
  titleRow.appendChild(titleSpan);

  const actionsContainer = document.createElement("div");
  actionsContainer.className = "card-actions";
  
  const moreActionsButton = document.createElement("button");
  moreActionsButton.className = "card-actions-button";
  moreActionsButton.dataset.id = task.id;
  
  const moreIcon = document.createElement("img");
  moreIcon.src = "/icons/MoreVert.svg";
  moreIcon.className = "btn-icon-img";
  moreIcon.alt = "Más";
  
  moreActionsButton.appendChild(moreIcon);
  actionsContainer.appendChild(moreActionsButton);

  // Actions popup
  const actionsPopup = document.createElement("div");
  actionsPopup.className = "card-actions-popup";
  actionsPopup.id = `popup-${task.id}`;

  // Edit button
  const editButton = document.createElement("button");
  editButton.className = "card-actions-popup-edit";
  editButton.dataset.id = task.id;
  editButton.innerHTML = `<img src="/icons/Edit.svg" class="popup-icon" alt="Editar"/> Editar`;
  actionsPopup.appendChild(editButton);

  // Delete button
  const deleteButton = document.createElement("button");
  deleteButton.className = "card-actions-popup-delete";
  deleteButton.dataset.id = task.id;
  deleteButton.innerHTML = `<img src="/icons/Delete.svg" class="popup-icon" alt="Eliminar"/> Eliminar`;
  actionsPopup.appendChild(deleteButton);

  // Status change buttons
  const statuses = TaskService.getAllStatuses();
  statuses.filter(s => s.id !== task.statusId).forEach(status => {
    const statusChangeButton = document.createElement("button");
    statusChangeButton.className = "card-actions-popup-status";
    statusChangeButton.dataset.id = task.id;
    statusChangeButton.dataset.statusId = status.id;
    
    let statusIcon = "/icons/Schedule.svg";
    if (status.name === "En progreso") statusIcon = "/icons/Autorenew.svg";
    else if (status.name === "Completada") statusIcon = "/icons/CheckCircle.svg";
    
    statusChangeButton.innerHTML = `<img src="${statusIcon}" class="popup-icon" alt="${status.name}"/> ${status.name}`;
    actionsPopup.appendChild(statusChangeButton);
  });

  actionsContainer.appendChild(actionsPopup);
  titleRow.appendChild(actionsContainer);
  cardElement.appendChild(titleRow);

  // Info row with status badge and date
  const infoRow = document.createElement("div");
  infoRow.className = "card-info-row";
  
  const badge = document.createElement("span");
  badge.className = "card-badge";
  
  const stateIndicator = document.createElement("span");
  const statusName = statuses.find(s => s.id === task.statusId)?.name || "Pendiente";
  
  if (statusName === "Pendiente") stateIndicator.className = "card-state-pendiente";
  else if (statusName === "En progreso") stateIndicator.className = "card-state-progreso";
  else stateIndicator.className = "card-state-completada";
  
  const stateLabel = document.createElement("span");
  stateLabel.className = "card-badge-text";
  stateLabel.textContent = statusName;
  
  badge.appendChild(stateIndicator);
  badge.appendChild(stateLabel);
  infoRow.appendChild(badge);

  // Date information
  const dateInfo = document.createElement("span");
  dateInfo.className = "card-date";
  
  const dateIcon = document.createElement("img");
  dateIcon.src = "/icons/CalendarToday.svg";
  dateIcon.className = "card-date-icon";
  dateIcon.alt = "Fecha";
  
  dateInfo.appendChild(dateIcon);
  dateInfo.append(task.dueDate ? new Date(task.dueDate).toLocaleDateString() : "Sin fecha");
  infoRow.appendChild(dateInfo);
  cardElement.appendChild(infoRow);

  // Description if available
  if (task.description) {
    const descriptionDiv = document.createElement("div");
    descriptionDiv.className = "card-desc";
    descriptionDiv.textContent = task.description;
    cardElement.appendChild(descriptionDiv);
  }

  // Users row for assigned users
  const usersRow = document.createElement("div");
  usersRow.className = "card-users-row";

  // Add assigned users avatars
  if (task.assignedUsers && task.assignedUsers.length > 0) {
    task.assignedUsers.forEach(user => {
      const userImg = document.createElement("img");
      userImg.src = user.profileImage;
      userImg.alt = user.name;
      userImg.title = user.name;
      userImg.className = "card-user-icon";
      usersRow.appendChild(userImg);
    });
  }

  // Add creator's avatar if available
  if (task.createdByUser) {
    const creatorContainer = document.createElement("div");
    creatorContainer.className = "card-users-author";
    
    const creatorImg = document.createElement("img");
    creatorImg.src = task.createdByUser.profileImage;
    creatorImg.alt = task.createdByUser.name;
    creatorImg.title = `Creado por: ${task.createdByUser.name}`;
    creatorImg.className = "card-users-author-icon";
    
    creatorContainer.appendChild(creatorImg);
    usersRow.appendChild(creatorContainer);
  }

  cardElement.appendChild(usersRow);
  
  return cardElement;
}

function showTaskForm(task) {
  if (document.getElementById("modal-background")) return;
  
  // Promise.all to fetch both users and statuses
  Promise.all([
    TaskService.getAllUsers(),
    TaskService.getAllStatuses()
  ]).then(([userList, statusList]) => {
    const isEditMode = !!task;

    // Create modal background and container
    const modalBackground = document.createElement("div");
    modalBackground.id = "modal-background";
    modalBackground.className = "modal-bg";

    const modalDialog = document.createElement("div");
    modalDialog.className = "modal";
    modalBackground.appendChild(modalDialog);

    // Close button
    const closeModalButton = document.createElement("button");
    closeModalButton.id = "close-modal-btn";
    closeModalButton.className = "modal-close";
    closeModalButton.innerHTML = `<img src="/icons/Close.svg" class="btn-icon-img" alt="Cerrar"/>`;
    modalDialog.appendChild(closeModalButton);

    // Modal title
    const titleHeading = document.createElement("h2");
    titleHeading.className = "modal-title";
    titleHeading.innerHTML = `<img src="/icons/${isEditMode ? 'Edit' : 'AddTask'}.svg" class="modal-title-icon" alt="${isEditMode ? 'Editar' : 'Añadir'}"/> ${
      isEditMode ? "Editar tarea" : "Nueva tarea"
    }`;
    modalDialog.appendChild(titleHeading);

    // Form element
    const formElement = document.createElement("form");
    formElement.id = "task-form";
    formElement.className = "form";
    modalDialog.appendChild(formElement);

    // Form fields definition
    const formGroups = [];

    // Title field
    const inputTitle = document.createElement("input");
    inputTitle.id = "title";
    inputTitle.name = "title";
    inputTitle.type = "text";
    inputTitle.required = true;
    inputTitle.minLength = 2;
    inputTitle.maxLength = 200;
    inputTitle.className = "input";
    if (task?.title) inputTitle.value = task.title;
    formGroups.push({ icon: "/icons/Label.svg", label: 'Título <span class="form-required">*</span>', input: inputTitle });

    // Description field
    const textareaDescription = document.createElement("textarea");
    textareaDescription.id = "description";
    textareaDescription.name = "description";
    textareaDescription.rows = 2;
    textareaDescription.className = "input";
    if (task?.description) textareaDescription.value = task.description;
    formGroups.push({ icon: "/icons/Description.svg", label: "Descripción", input: textareaDescription });

    // Due date field
    const inputDueDate = document.createElement("input");
    inputDueDate.id = "dueDate";
    inputDueDate.name = "dueDate";
    inputDueDate.type = "date";
    inputDueDate.className = "input";
    if (task?.dueDate) {
      const dueDate = new Date(task.dueDate);
      inputDueDate.value = dueDate.toISOString().substring(0, 10);
    }
    formGroups.push({ icon: "/icons/CalendarToday.svg", label: "Fecha de vencimiento", input: inputDueDate });

    // User assignment field
    const selectAssignedUsers = document.createElement("select");
    selectAssignedUsers.id = "assignedUserIds";
    selectAssignedUsers.name = "assignedUserIds";
    selectAssignedUsers.multiple = true;
    selectAssignedUsers.required = true;
    selectAssignedUsers.className = "input";
    selectAssignedUsers.size = Math.min(userList.length, 5);
    
    userList.forEach(user => {
      const option = document.createElement("option");
      option.value = user.id;
      option.textContent = user.name;
      
      // If editing a task, check if this user is assigned to the task
      if (task && task.assignedUsers) {
        const isAssigned = task.assignedUsers.some(assignedUser => assignedUser.id === user.id);
        option.selected = isAssigned;
      }
      
      selectAssignedUsers.appendChild(option);
    });
    
    formGroups.push({ 
      icon: "/icons/People.svg", 
      label: 'Usuarios asignados <span class="form-required">*</span>', 
      input: selectAssignedUsers 
    });

    // Status field (only for edit mode)
    if (isEditMode && task) {
      const selectStatus = document.createElement("select");
      selectStatus.id = "statusId";
      selectStatus.name = "statusId";
      selectStatus.required = true;
      selectStatus.className = "input";
      statusList.forEach(s => {
        const option = document.createElement("option");
        option.value = s.id;
        option.textContent = s.name;
        if (task.statusId === s.id) option.selected = true;
        selectStatus.appendChild(option);
      });
      formGroups.push({ icon: "/icons/ViewList.svg", label: 'Estado <span class="form-required">*</span>', input: selectStatus });
    }

    // Render form groups
    formGroups.forEach(group => {
      const groupDiv = document.createElement("div");
      groupDiv.className = "form-group";
      
      const labelElement = document.createElement("label");
      labelElement.className = "form-label";
      labelElement.htmlFor = group.input.id;
      labelElement.innerHTML = `<img src="${group.icon}" class="form-label-icon" alt=""/> ${group.label}`;
      
      groupDiv.appendChild(labelElement);
      groupDiv.appendChild(group.input);
      formElement.appendChild(groupDiv);
    });

    // Form action buttons
    const formActions = document.createElement("div");
    formActions.className = "form-actions";
    
    const cancelFormButton = document.createElement("button");
    cancelFormButton.type = "button";
    cancelFormButton.id = "cancel-btn";
    cancelFormButton.className = "form-cancel-button";
    cancelFormButton.innerHTML = `<img src="/icons/Close.svg" class="btn-icon-img" alt="Cancelar"/> Cancelar`;
    
    const submitFormButton = document.createElement("button");
    submitFormButton.type = "submit";
    submitFormButton.className = "form-submit-button";
    submitFormButton.innerHTML = `<img src="/icons/Save.svg" class="btn-icon-img" alt="Guardar"/> Guardar`;
    
    formActions.appendChild(cancelFormButton);
    formActions.appendChild(submitFormButton);
    formElement.appendChild(formActions);

    // Add modal to document
    document.body.appendChild(modalBackground);

    // Event handlers
    closeModalButton.addEventListener("click", () => modalBackground.remove());
    cancelFormButton.addEventListener("click", () => modalBackground.remove());

    formElement.onsubmit = e => {
      e.preventDefault();
      const formData = new FormData(formElement);
      const title = formData.get("title").toString().trim();
      const description = formData.get("description").toString().trim() || undefined;
      const dueDate = formData.get("dueDate")?.toString() || undefined;
      const statusIdFromForm = formData.get("statusId")?.toString();
      
      // Get selected users
      const assignedUserIds = Array.from(selectAssignedUsers.selectedOptions).map(option => option.value);
      
      if (!title) {
        alert("El título es obligatorio.");
        return;
      }
      
      if (assignedUserIds.length === 0) {
        alert("Debe seleccionar al menos un usuario asignado.");
        return;
      }

      if (isEditMode && task) {
        // Update existing task
        const updateData = {
          id: task.id,
          title,
          description,
          dueDate: dueDate ? new Date(dueDate).toISOString() : null,
          statusId: statusIdFromForm || task.statusId,
          assignedUserIds // Include assigned users in update
        };
        
        // Call API to update task with assigned users
        TaskService.updateTask(updateData)
          .then(() => {
            modalBackground.remove();
            renderTaskBoard();
          })
          .catch(error => {
            console.error("Error updating task:", error);
            alert("Error al actualizar la tarea: " + error.message);
          });
      } else {
        // Create new task
        const firstUser = userList && userList.length > 0 ? userList[0] : null;
        const creatorId = firstUser ? firstUser.id : null;
        
        if (!creatorId) {
          alert("No hay usuarios disponibles para asignar como creador.");
          return;
        }

        const newTaskData = {
          title,
          description,
          dueDate: dueDate ? new Date(dueDate).toISOString() : null,
          statusId: statusList[0].id, // Use first status (Pendiente)
          createdBy: creatorId,
          assignedUserIds // Include assigned users in creation
        };
        
        // Call API to create task with assigned users
        TaskService.createTask(newTaskData)
          .then(() => {
            modalBackground.remove();
            renderTaskBoard();
          })
          .catch(error => {
            console.error("Error creating task:", error);
            alert("Error al crear la tarea: " + error.message);
          });
      }
    };
  }).catch(error => {
    console.error("Error preparing task form:", error);
    alert("Error al cargar los datos necesarios para el formulario.");
  });
}

function showConfirmDialog(taskId) {
  if (document.getElementById("modal-background")) return;

  const modalBackground = document.createElement("div");
  modalBackground.id = "modal-background";
  modalBackground.className = "modal-bg";

  const modalDialog = document.createElement("div");
  modalDialog.className = "modal";
  modalBackground.appendChild(modalDialog);

  const titleElement = document.createElement("h2");
  titleElement.className = "modal-title";
  titleElement.textContent = "¿Eliminar tarea?";
  modalDialog.appendChild(titleElement);

  const messageElement = document.createElement("p");
  messageElement.textContent = "¿Estás seguro de que deseas eliminar esta tarea? Esta acción no se puede deshacer.";
  modalDialog.appendChild(messageElement);

  const actionsContainer = document.createElement("div");
  actionsContainer.className = "confirm-actions";
  
  const cancelDeleteButton = document.createElement("button");
  cancelDeleteButton.id = "cancel-delete-btn";
  cancelDeleteButton.className = "confirm-cancel-button";
  cancelDeleteButton.textContent = "Cancelar";
  
  const confirmDeleteButton = document.createElement("button");
  confirmDeleteButton.id = "confirm-delete-btn";
  confirmDeleteButton.className = "confirm-delete-button";
  confirmDeleteButton.textContent = "Eliminar";
  
  actionsContainer.appendChild(cancelDeleteButton);
  actionsContainer.appendChild(confirmDeleteButton);
  modalDialog.appendChild(actionsContainer);

  document.body.appendChild(modalBackground);

  cancelDeleteButton.addEventListener("click", () => modalBackground.remove());
  confirmDeleteButton.addEventListener("click", () => {
    TaskService.deleteTask(taskId)
      .then(() => {
        modalBackground.remove();
        renderTaskBoard();
      })
      .catch(error => {
        console.error("Error deleting task:", error);
        alert("Error al eliminar la tarea: " + error.message);
      });
  });
}

// Setup global click handler to close popups
document.addEventListener("click", () => {
  document.querySelectorAll(".card-actions-popup")
    .forEach(popup => {
      popup.classList.remove("card-actions-popup-visible");
    });
});

// Initialize the app when DOM is loaded
window.addEventListener("DOMContentLoaded", () => {
  const addTaskButton = document.getElementById("add-task-btn");
  if (addTaskButton) {
    addTaskButton.addEventListener("click", () => showTaskForm());
  }
  
  // Setup drag and drop for columns
  const statusList = TaskService.getAllStatuses();
  statusList.forEach(status => {
    // Use full status ID without substring
    const column = document.getElementById(`col-${status.id}`);
    if (!column) return;
    
    column.addEventListener("dragover", e => {
      e.preventDefault();
      column.classList.add("drop-hover");
    });
    
    column.addEventListener("dragleave", () => {
      column.classList.remove("drop-hover");
    });
    
    column.addEventListener("drop", e => {
      e.preventDefault();
      column.classList.remove("drop-hover");
      const taskId = e.dataTransfer?.getData("text/plain");
      if (taskId) {
        TaskService.updateTaskStatus({ id: taskId, statusId: status.id })
          .then(() => renderTaskBoard())
          .catch(err => console.error("Error updating task status via drag & drop:", err));
      }
    });
  });
  
  // Initial render
  renderTaskBoard();
});