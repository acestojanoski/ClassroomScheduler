import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from 'environments/environment';
import { Building } from 'models/building.model';

@Injectable()
export class BuildingService {

  constructor(
    private http: HttpClient
  ) { }

  createBuilding(building: Building): Observable<Building> {
    return this.http.post<Building>(`${environment.apiUrl}/Buildings`, building);
  }

  updateBuilding(id: number, building: Building) {
    return this.http.put<any>(`${environment.apiUrl}/Buildings/${id}`, building);
  }

  deleteBuilding(id: number) {
    return this.http.delete<any>(`${environment.apiUrl}/Buildings/${id}`);
  }

  getBuildingById(id: number): Observable<Building> {
    return this.http.get<Building>(`${environment.apiUrl}/Buildings/${id}`);
  }

  getBuildings(): Observable<Building[]> {
    return this.http.get<Building[]>(`${environment.apiUrl}/Buildings`);
  }

}
