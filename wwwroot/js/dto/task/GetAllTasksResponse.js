export class GetAllTasksResponse {
  constructor({ id, title, description, dueDate, statusId, createdBy, createdByUser, createdAt, updatedAt, assignedUsers }) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.dueDate = dueDate;
    this.statusId = statusId;
    this.createdBy = createdBy;
    this.createdByUser = createdByUser;
    this.createdAt = createdAt;
    this.updatedAt = updatedAt;
    this.assignedUsers = assignedUsers;
  }
}