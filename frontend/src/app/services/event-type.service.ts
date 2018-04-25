import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from 'environments/environment';
import { EventType } from 'models/event-type.model';

@Injectable()
export class EventTypeService {

  constructor(
    private http: HttpClient
  ) { }

  createEventType(eventType: EventType): Observable<EventType> {
    return this.http.post<EventType>(`${environment.apiUrl}/EventTypes`, eventType);
  }

  updateEventType(id: number, eventType: EventType) {
    return this.http.put<any>(`${environment.apiUrl}/EventTypes/${id}`, eventType);
  }

  deleteEventType(id: number) {
    return this.http.delete<any>(`${environment.apiUrl}/EventTypes/${id}`);
  }

  getEventTypeById(id: number): Observable<EventType> {
    return this.http.get<EventType>(`${environment.apiUrl}/EventTypes/${id}`);
  }

  getEventTypes(): Observable<EventType[]> {
    return this.http.get<EventType[]>(`${environment.apiUrl}/EventTypes`);
  }

}
