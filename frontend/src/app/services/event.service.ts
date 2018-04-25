import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from 'environments/environment';
import { Event } from 'models/event.model';

@Injectable()
export class EventService {

  constructor(
    private http: HttpClient
  ) { }

  createEvent(event: Event): Observable<Event> {
    return this.http.post<Event>(`${environment.apiUrl}/Events`, event);
  }

  updateEvent(id: number, event: Event) {
    return this.http.put<any>(`${environment.apiUrl}/Events/${id}`, event);
  }

  deleteEvent(id: number) {
    return this.http.delete<any>(`${environment.apiUrl}/Events/${id}`);
  }

  getEventById(id: number): Observable<Event> {
    return this.http.get<Event>(`${environment.apiUrl}/Events/${id}`);
  }

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(`${environment.apiUrl}/Events`);
  }

}
