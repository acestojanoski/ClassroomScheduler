import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NglModule } from 'ng-lightning/ng-lightning';
import { CalendarModule } from 'angular-calendar';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';

import { EventLayoutComponent } from './event-layout/event-layout.component';
import { CreateEventComponent } from './create-event/create-event.component';
import { CreateEventTypeComponent } from './create-event-type/create-event-type.component';
import { EventTypeLayoutComponent } from './event-type-layout/event-type-layout.component';
import { LayoutComponent } from './layout/layout.component';

const routes: Routes = [
  { path: '', component: LayoutComponent }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    NglModule,
    CalendarModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule
  ],
  declarations: [
    EventLayoutComponent,
    CreateEventComponent,
    CreateEventTypeComponent,
    EventTypeLayoutComponent,
    LayoutComponent
  ]
})
export class EventModule { }
