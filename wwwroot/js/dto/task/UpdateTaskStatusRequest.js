export class UpdateTaskStatusRequest {
  constructor({ id, statusId }) {
    this.id = id;
    this.statusId = statusId;
  }
}