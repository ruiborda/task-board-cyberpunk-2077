export class GetTaskByIdResponse {
  constructor({ id, title, description, dueDate, statusId, createdBy, createdAt, updatedAt, assignedUsers = [] }) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.dueDate = dueDate;
    this.statusId = statusId;
    this.createdBy = createdBy;
    this.createdAt = createdAt;
    this.updatedAt = updatedAt;
    this.assignedUsers = assignedUsers;
  }
}