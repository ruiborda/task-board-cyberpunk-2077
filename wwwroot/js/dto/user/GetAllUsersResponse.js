export class GetAllUsersResponse {
  constructor({ id, name, email, profileImage }) {
    this.id = id;
    this.name = name;
    this.email = email;
    this.profileImage = profileImage;
  }
}