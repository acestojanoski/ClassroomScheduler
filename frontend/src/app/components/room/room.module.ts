import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { NglModule } from 'ng-lightning/ng-lightning';

import {ClassroomLayoutComponent} from './classroom/classroom-layout/classroom-layout.component';
import {CreateClassroomComponent} from './classroom/create-classroom/create-classroom.component';
import { BuildingLayoutComponent } from './building/building-layout/building-layout.component';
import { CreateBuildingComponent } from './building/create-building/create-building.component';
import { RoomComponent } from './room.component';

const routes: Routes = [
  { path: '', component: RoomComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    NglModule
  ],
  declarations: [
    ClassroomLayoutComponent,
    CreateClassroomComponent,
    BuildingLayoutComponent,
    CreateBuildingComponent,
    RoomComponent
  ]
})
export class RoomModule { }
