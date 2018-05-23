import { Component, OnInit } from '@angular/core';

import 'rxjs/add/operator/finally';

import { ClassRoomService } from 'services/class-room.service';
import { ClassRoom } from 'models/class-room.model';

@Component({
  selector: 'crs-classroom-layout',
  templateUrl: './classroom-layout.component.html',
  styleUrls: ['./classroom-layout.component.scss']
})
export class ClassroomLayoutComponent implements OnInit {

  public classrooms: ClassRoom[] = [];
  public loading = true;

  constructor(private classroomsService: ClassRoomService) { }

  ngOnInit() {
    this.getClassrooms();
  }

  public getClassrooms() {
    this.classroomsService.getClassRooms()
      .finally(() => this.loading = false)
      .subscribe(res => this.classrooms = res, err => console.error(err));
  }

}
