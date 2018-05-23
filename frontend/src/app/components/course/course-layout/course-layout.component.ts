import { Component, OnInit } from '@angular/core';

import 'rxjs/add/operator/finally';

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

  public mapProfessors(professors) {
    return professors.map(p => p.professor.firstName + ' ' + p.professor.lastName).join(', ');
  }

  public getCourses() {
    this.courseService.getCourses()
      .finally(() => this.loading = false)
      .subscribe(res => this.courses = res, err => console.error(err));
  }

}
