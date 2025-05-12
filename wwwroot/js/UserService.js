import { GetAllUsersResponse } from './dto/user/GetAllUsersResponse.js';

const baseUrl = '/api/UserInputAdapter';

export class UserService {
  /**
   * Fetch all users from the API.
   * @returns {Promise<GetAllUsersResponse[]>}
   */
  static async getAllUsers() {
    const res = await fetch(baseUrl);
    if (!res.ok) throw new Error(`Error fetching users: ${res.statusText}`);
    const data = await res.json();
    return data.map(item => new GetAllUsersResponse(item));
  }

  /**
   * Get user by ID from the API.
   * @param {string} id - User ID to fetch
   * @returns {Promise<GetAllUsersResponse>}
   */
  static async getUserById(id) {
    const res = await fetch(`${baseUrl}/${id}`);
    if (!res.ok) throw new Error(`Error fetching user ${id}: ${res.statusText}`);
    const data = await res.json();
    return new GetAllUsersResponse(data);
  }
  
  /**
   * Returns all available user profile images based on user's name.
   * @param {string} name - User name used as seed for the avatar
   * @returns {string} URL to the profile image
   */
  static getProfileImageUrl(name) {
    return `https://api.dicebear.com/9.x/adventurer/svg?seed=${encodeURIComponent(name)}`;
  }
}