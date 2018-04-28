import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { JwtHelperService } from '@auth0/angular-jwt';

import { environment } from 'environments/environment';

@Injectable()
export class AuthService {

  public loggedIn: Subject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(
    private http: HttpClient
  ) { }

  login(data) {
    return this.http.post<any>(`${environment.apiUrl}/Authentication/Login`, data);
  }

  public validateToken() {
    const helper = new JwtHelperService();
    this.loggedIn.next(!helper.isTokenExpired(this.getToken()));
  }

  public getToken() {
    return localStorage.getItem('access_token');
  }

  public setSession(token) {
    this.loggedIn.next(true);
    return localStorage.setItem('access_token', token);
  }

  public destroySession() {
    this.loggedIn.next(false);
    localStorage.clear();
  }

}
