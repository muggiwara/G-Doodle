import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API } from '../app.settings';
import { User } from './user';

@Injectable()
export class UserService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(`${API}api/user`);
  }

  getById(id: number) {
    return this.http.get(`${API}api/user/${id}`);
  }

  create(user: User) {
    return this.http.post(`${API}api/user`, user);
  }

  update(user: User) {
    return this.http.put(`${API}api/user`, user);
  }

  delete(id: number) {
    return this.http.delete(`${API}api/user/${id}`);
  }

}
