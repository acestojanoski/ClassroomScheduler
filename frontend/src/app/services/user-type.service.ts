import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from 'environments/environment';
import { UserType } from 'models/user-type.model';

@Injectable()
export class UserTypeService {

  constructor(
    private http: HttpClient
  ) { }

  createUserType(userType: UserType): Observable<UserType> {
    return this.http.post<UserType>(`${environment.apiUrl}/UserTypes`, userType);
  }

  updateUserType(id: number, userType: UserType) {
    return this.http.put<any>(`${environment.apiUrl}/UserTypes/${id}`, userType);
  }

  deleteUserType(id: number) {
    return this.http.delete<any>(`${environment.apiUrl}/UserTypes/${id}`);
  }

  getUserTypeById(id: number): Observable<UserType> {
    return this.http.get<UserType>(`${environment.apiUrl}/UserTypes/${id}`);
  }

  getUserTypes(): Observable<UserType[]> {
    return this.http.get<UserType[]>(`${environment.apiUrl}/UserTypes`);
  }

}
