import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { NglModule } from 'ng-lightning/ng-lightning';

import { CreateCourseComponent } from './create-course/create-course.component';
import { CourseLayoutComponent } from './course-layout/course-layout.component';

const routes: Routes = [
  { path: '', component: CourseLayoutComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    NglModule
  ],
  declarations: [
    CreateCourseComponent,
    CourseLayoutComponent
  ]
})
export class CourseModule { }
