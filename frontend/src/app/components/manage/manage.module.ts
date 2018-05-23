import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';
import {NglModule} from 'ng-lightning';
import {UserManagerComponent} from './user-manager/user-manager.component';
import {UserTypeLayoutComponent} from 'app/components/manage/user-type/user-type-layout/user-type-layout.component';
import {CreateUserTypeComponent} from './user-type/create-user-type/create-user-type.component';
import {ManageComponent} from './manage.component';

const routes: Routes = [
  { path: '', component: ManageComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    NglModule
  ],
  declarations: [
    ManageComponent,
    UserManagerComponent,
    UserTypeLayoutComponent,
    CreateUserTypeComponent,
  ]
})
export class ManageModule { }
