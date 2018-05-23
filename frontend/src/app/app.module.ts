// core libraries
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

// external libraries
import { NglModule } from 'ng-lightning/ng-lightning';
import { CalendarModule } from 'angular-calendar';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';

// routing module
import { AppRoutingModule } from './app-routing.module';

// guards
import { AuthGuard } from 'guards/auth.guard';

// interceptors
import { TokenInterceptor } from 'interceptors/token.interceptor';

// services
import { AuthService } from 'services/auth.service';
import { BuildingService } from 'services/building.service';
import { ClassRoomService } from 'services/class-room.service';
import { CourseService } from 'services/course.service';
import { EventTypeService } from 'services/event-type.service';
import { EventService } from 'services/event.service';
import { UserTypeService } from 'services/user-type.service';
import { UserService } from 'services/user.service';

// components
import { AppComponent } from './app.component';
import { NavComponent } from './core/nav/nav.component';
import { ManageComponent } from './components/manage/manage.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    NglModule.forRoot({
      svgPath: '../assets/icons'
    }),
    CalendarModule.forRoot(),
    OwlDateTimeModule,
    OwlNativeDateTimeModule
  ],
  providers: [
    AuthGuard,
    AuthService,
    BuildingService,
    ClassRoomService,
    CourseService,
    EventTypeService,
    EventService,
    UserTypeService,
    UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
