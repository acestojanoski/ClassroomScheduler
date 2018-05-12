import { Component, OnInit } from '@angular/core';
import {ClassRoomService} from 'services/class-room.service';
import {ClassRoom} from 'models/class-room.model';

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
    this.classroomsService.getClassRooms().subscribe(res => {
      this.classrooms = res;
      this.loading = false;
      console.log(this.classrooms);
    }, err => console.error(err));
  }

}
