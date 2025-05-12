export class UpdateTaskRequest {
  constructor({ id, title, description, dueDate, statusId, assignedUserIds = [] }) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.dueDate = dueDate instanceof Date ? dueDate.toISOString() : dueDate;
    this.statusId = statusId;
    this.assignedUserIds = assignedUserIds;
  }
}