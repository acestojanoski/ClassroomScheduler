// core libraries
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

// external libraries
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NglModule } from 'ng-lightning/ng-lightning';

// routing module
import { AppRoutingModule } from './app-routing.module';

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

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    NglModule.forRoot()
  ],
  providers: [
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
