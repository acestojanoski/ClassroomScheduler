import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { JwtHelperService } from '@auth0/angular-jwt';

import { environment } from 'environments/environment';

@Injectable()
export class AuthService {

  public user: Subject<any> = new BehaviorSubject<any>(null);
  private jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(
    private http: HttpClient
  ) { }

  login(data) {
    return this.http.post<any>(`${environment.apiUrl}/Authentication/Login`, data);
  }

  public validateToken() {
    const value = !this.jwtHelper.isTokenExpired(this.getToken()) ? this.jwtHelper.decodeToken(this.getToken()) : null;
    this.user.next(value);
  }

  public getToken() {
    return localStorage.getItem('access_token');
  }

  public setSession(token) {
    this.user.next(this.jwtHelper.decodeToken(token));
    return localStorage.setItem('access_token', token);
  }

  public destroySession() {
    this.user.next(null);
    localStorage.clear();
  }

}
