import { Component, OnInit, Output, EventEmitter} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { CourseService } from 'services/course.service';
import { UserService } from 'services/user.service';

@Component({
  selector: 'crs-create-course',
  templateUrl: './create-course.component.html',
  styleUrls: ['./create-course.component.scss']
})
export class CreateCourseComponent implements OnInit {

  @Output() public newCourse: EventEmitter<any> = new EventEmitter<any>();
  public courseForm: FormGroup;
  public opened = false;
  public courseId: number;
  public semesters = [1, 2, 3, 4, 5, 6, 7, 8];
  public professors = [];
  public pick = [];
  public picklistOpen = false;

  constructor(
    private courseService: CourseService,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.courseForm = this.initCourseForm();
    this.getUsers();
  }

  public open(courseId = null) {
    this.courseId = courseId;
    this.opened = true;
    this.courseForm.reset();
    this.pick = [];
    if (this.courseId) {
      this.getCourseById(this.courseId);
    }
  }

  submit() {
    if (this.courseForm.invalid || this.pick.length < 1) { return; }

    const course = Object.assign({}, this.courseForm.value);
    course.professorsId = this.pick.map(p => p.id);

    if (this.courseId) {
      this.updateCourse(this.courseId, course);
    } else {
      this.createCourse(course);
    }
  }

  private createCourse(course) {
    this.courseService.createCourse(course).subscribe(res => {
      this.opened = false;
      this.newCourse.emit();
    }, err => console.error(err));
  }

  private updateCourse(id, course) {
    this.courseService.updateCourse(id, course).subscribe(res => {
      this.opened = false;
      this.newCourse.emit();
    }, err => console.error(err));
  }

  private getCourseById(id) {
    this.courseService.getCourseById(id).subscribe(res => this.courseForm.patchValue(res), err => console.error(err));
  }

  private getUsers() {
    this.userService.getAllUsers().subscribe(res => {
      this.professors = res.map(p => {
        const obj = { name: `${p.firstName} ${p.lastName}`, id: p.id };
        return obj;
      });
    }, err => console.error(err));
  }

  private initCourseForm() {
    return new FormGroup({
      name: new FormControl(null, Validators.required),
      semester: new FormControl(1, Validators.required)
    });
  }

  get pickLabel() {
    return this.pick && this.pick.length ? this.pick.map(p => p.name).join(', ') : 'Select professor(s)';
  }

  get name() { return this.courseForm.get('name'); }
  get semester() { return this.courseForm.get('semester'); }
}
