import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from 'guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: 'events', loadChildren: './components/event/event.module#EventModule', canActivate: [AuthGuard] },
  { path: 'courses', loadChildren: './components/course/course.module#CourseModule', canActivate: [AuthGuard] },
  { path: 'rooms', loadChildren: './components/room/room.module#RoomModule', canActivate: [AuthGuard] },
  { path: 'register', loadChildren: './components/register/register.module#RegisterModule' },
  { path: 'login', loadChildren: './components/login/login.module#LoginModule' },
  { path: 'usermanager', loadChildren: './components/manage/manage.module#ManageModule' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
