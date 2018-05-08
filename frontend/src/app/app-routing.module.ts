import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: 'login', loadChildren: './components/login/login.module#LoginModule' },
  { path: 'register', loadChildren: './components/register/register.module#RegisterModule' },
  { path: 'courses', loadChildren: './components/course/course.module#CourseModule' },
  { path: 'classrooms', loadChildren: './components/room/room.module#RoomModule'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
