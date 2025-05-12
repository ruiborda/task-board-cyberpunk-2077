import { CreateTaskRequest } from './dto/task/CreateTaskRequest.js';
import { CreateTaskResponse } from './dto/task/CreateTaskResponse.js';
import { GetAllTasksResponse } from './dto/task/GetAllTasksResponse.js';
import { GetTaskByIdResponse } from './dto/task/GetTaskByIdResponse.js';
import { UpdateTaskRequest } from './dto/task/UpdateTaskRequest.js';
import { UpdateTaskResponse } from './dto/task/UpdateTaskResponse.js';
import { UpdateTaskStatusRequest } from './dto/task/UpdateTaskStatusRequest.js';

const baseUrl = '/api/TaskInputAdapter';

export class TaskService {
  static async getAllTasks() {
    const res = await fetch(baseUrl);
    if (!res.ok) throw new Error(`Error fetching tasks: ${res.statusText}`);
    const data = await res.json();
    return data.map(item => new GetAllTasksResponse(item));
  }

  static async getTaskById(id) {
    const res = await fetch(`${baseUrl}/${id}`);
    if (!res.ok) throw new Error(`Error fetching task ${id}: ${res.statusText}`);
    const data = await res.json();
    return new GetTaskByIdResponse(data);
  }

  /**
   * Creates a new task.
   * @returns {Promise<CreateTaskResponse>}
   * @param {CreateTaskRequest} requestDto - The task data to create.
   */

  static async createTask(requestDto) {
    const req = requestDto instanceof CreateTaskRequest ? requestDto : new CreateTaskRequest(requestDto);
    const res = await fetch(baseUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(req)
    });
    if (!res.ok) throw new Error(`Error creating task: ${res.statusText}`);
    const data = await res.json();
    return new CreateTaskResponse(data);
  }

  static async updateTask(requestDto) {
    const req = requestDto instanceof UpdateTaskRequest ? requestDto : new UpdateTaskRequest(requestDto);
    const res = await fetch(baseUrl, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(req)
    });
    if (!res.ok) throw new Error(`Error updating task: ${res.statusText}`);
    const data = await res.json();
    return new UpdateTaskResponse(data);
  }

  static async deleteTask(id) {
    const res = await fetch(`${baseUrl}/${id}`, { method: 'DELETE' });
    if (!res.ok) throw new Error(`Error deleting task ${id}: ${res.statusText}`);
    const result = await res.json();
    return result; 
  }

  static async updateTaskStatus(requestDto) {
    const req = requestDto instanceof UpdateTaskStatusRequest ? requestDto : new UpdateTaskStatusRequest(requestDto);
    const res = await fetch(`${baseUrl}/UpdateTaskStatus`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(req)
    });
    if (!res.ok) throw new Error(`Error updating task status: ${res.statusText}`);
    const data = await res.json();
    return new UpdateTaskResponse(data);
  }

  /**
   * Get all possible task statuses.
   * @returns {{id: string, name: string}[]}
   */
  static getAllStatuses() {
    return [
      { id: '5d288d05-fabc-4c31-a3a5-efc0c65fcd03', name: 'Pendiente' },
      { id: 'e1f7b815-e9eb-48c1-a487-d90097a5b03f', name: 'En progreso' },
      { id: 'e969c3bc-2ffd-4315-9da3-ce6ebc3f4599', name: 'Completada' }
    ];
  }

  /**
   * Fetch all users from API.
   * @returns {Promise<{id: string, name: string, profileImage: string}[]>}
   */
  static async getAllUsers() {
    const res = await fetch('/api/UserInputAdapter');
    if (!res.ok) throw new Error(`Error fetching users: ${res.statusText}`);
    return await res.json();
  }
}