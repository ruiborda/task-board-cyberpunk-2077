export class CreateTaskRequest {
  constructor({ title, description, dueDate, statusId, createdBy, assignedUserIds = [] }) {
    this.title = title;
    this.description = description;
    this.dueDate = dueDate instanceof Date ? dueDate.toISOString() : dueDate;
    this.statusId = statusId;
    this.createdBy = createdBy;
    this.assignedUserIds = assignedUserIds;
  }
}