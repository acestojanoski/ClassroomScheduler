import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from 'environments/environment';
import { ClassRoom } from 'models/class-room.model';

@Injectable()
export class ClassRoomService {

  constructor(
    private http: HttpClient
  ) { }

  createClassRoom(classRoom: ClassRoom): Observable<ClassRoom> {
    return this.http.post<ClassRoom>(`${environment.apiUrl}/ClassRooms`, classRoom);
  }

  updateClassRoom(id: number, classRoom: ClassRoom) {
    return this.http.put<any>(`${environment.apiUrl}/ClassRooms/${id}`, classRoom);
  }

  deleteClassRoom(id: number) {
    return this.http.delete<any>(`${environment.apiUrl}/ClassRooms/${id}`);
  }

  getClassRoomById(id: number): Observable<ClassRoom> {
    return this.http.get<ClassRoom>(`${environment.apiUrl}/ClassRooms/${id}`);
  }

  getClassRooms(): Observable<ClassRoom[]> {
    return this.http.get<ClassRoom[]>(`${environment.apiUrl}/ClassRooms`);
  }

}
