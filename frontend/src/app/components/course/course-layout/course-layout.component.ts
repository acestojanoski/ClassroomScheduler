import { Component, OnInit } from '@angular/core';

import { CourseService } from 'services/course.service';
import { Course } from 'models/course.model';

@Component({
  selector: 'crs-course-layout',
  templateUrl: './course-layout.component.html',
  styleUrls: ['./course-layout.component.scss']
})
export class CourseLayoutComponent implements OnInit {

  public courses: Course[] = [];
  public loading = true;

  constructor(
    private courseService: CourseService
  ) { }

  ngOnInit() {
    this.getCourses();
  }

  mapProfessors(professors) {
    return professors.map(p => p.professor.firstName + ' ' + p.professor.lastName).join(', ');
  }

  private getCourses() {
    this.courseService.getCourses().subscribe(res => {
      this.courses = res;
      this.loading = false
    }, err => console.error(err));
  }

}
