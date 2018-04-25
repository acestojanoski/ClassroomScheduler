import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'environments/environment';

@Injectable()
export class AuthService {

  constructor(
    private http: HttpClient
  ) { }

  login(data) {
    return this.http.post<any>(`${environment.apiUrl}/Authentication/Login`, data);
  }

  public getToken() {
    return localStorage.getItem('access_token');
  }

  public setSession(token) {
    return localStorage.setItem('access_token', token);
  }

  public destroySession() {
    localStorage.clear();
  }

}
