import { Injectable } from '@angular/core';
import {environment} from 'environments/environment';
import {HttpClient} from '@angular/common/http';
import {User} from 'models/user.model';

@Injectable()
export class ManageService {

  constructor(
    private http: HttpClient
  ) { }

  changePassword(data) {
    return this.http.post<any>(`${environment.apiUrl}/ManageUser/ChangePassword`, data);
  }

  updateUser(data) {
    return this.http.put<any>(`${environment.apiUrl}/ManageUser/UpdateUser`, data);
  }
}
