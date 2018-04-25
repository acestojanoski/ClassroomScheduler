import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from 'environments/environment';
import { Course } from 'models/course.model';

@Injectable()
export class CourseService {

  constructor(
    private http: HttpClient
  ) { }

  createCourse(course: Course): Observable<Course> {
    return this.http.post<Course>(`${environment.apiUrl}/Courses`, course);
  }

  updateCourse(id: number, course: Course) {
    return this.http.put<any>(`${environment.apiUrl}/Courses/${id}`, course);
  }

  deleteCourse(id: number) {
    return this.http.delete<any>(`${environment.apiUrl}/Courses/${id}`);
  }

  getCourseById(id: number): Observable<Course> {
    return this.http.get<Course>(`${environment.apiUrl}/Courses/${id}`);
  }

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${environment.apiUrl}/Courses`);
  }

}
