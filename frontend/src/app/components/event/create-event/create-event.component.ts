import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { EventService } from 'services/event.service';
import { EventTypeService } from 'services/event-type.service';
import { ClassRoomService } from 'services/class-room.service';
import { CourseService } from 'services/course.service';

import { EventType } from 'models/event-type.model';
import { ClassRoom } from 'models/class-room.model';
import { Course } from 'models/course.model';

@Component({
  selector: 'crs-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.scss']
})
export class CreateEventComponent implements OnInit {

  @Output() public newEvent: EventEmitter<any> = new EventEmitter<any>();
  public eventForm: FormGroup;
  public opened = false;
  public eventId: number;
  public repeatIntervals = [
    { key: 'no', title: 'None' },
    { key: 'day', title: 'Every day' },
    { key: 'week', title: 'Every week' },
    { key: 'month', title: 'Every month' }
  ];
  public eventTypes: EventType[] = [];
  public classRooms: ClassRoom[] = [];
  public courses: Course[] = [];
  public editing = false;

  constructor(
    private eventService: EventService,
    private eventTypeService: EventTypeService,
    private classRoomService: ClassRoomService,
    private courseService: CourseService
  ) { }

  ngOnInit() {
    this.eventForm = this.initEventForm();
    this.getEventTypes();
    this.getClassRooms();
    this.getCourses();
  }

  public open(eventId = null) {
    this.eventId = eventId;
    this.eventForm = this.initEventForm();
    this.opened = true;
    this.editing = false;
    if (this.eventId) {
      this.getEventById(this.eventId);
      this.editing = true;
    }
  }

  submit() {
    if (this.eventForm.invalid) { return; }

    const event = Object.assign({}, this.eventForm.value);
    event.repeat = event.repeatInterval !== 'no';

    if (this.eventId) {
      this.updateEvent(this.eventId, event);
    } else {
      this.createEvent(event);
    }
  }

  private createEvent(event) {
    this.eventService.createEvent(event).subscribe(res => {
      this.opened = false;
      this.newEvent.emit();
    });
  }

  private updateEvent(id, event) {
    this.eventService.updateEvent(id, event).subscribe(res => {
      this.opened = false;
      this.newEvent.emit();
    });
  }

  private deleteEvent() {
    this.eventService.deleteEvent(this.eventId).subscribe(res => {
      console.log(res);
      this.opened = false;
      this.newEvent.emit();
    }, err => {
      console.error(err);
    });
  }

  private getEventById(id) {
    this.eventService.getEventById(id).subscribe(res => {
      const ev: any = Object.assign({}, res);
      ev.eventTypeId = res.eventType.id;
      ev.courseId = res.course.id;
      ev.classRoomId = res.classRoom.id;
      this.eventForm.patchValue(ev);
    }, err => console.error(err));
  }

  private getEventTypes() {
    this.eventTypeService.getEventTypes().subscribe(res => this.eventTypes = res, err => console.error(err));
  }

  private getClassRooms() {
    this.classRoomService.getClassRooms().subscribe(res => this.classRooms = res, err => console.error(err));
  }

  private getCourses() {
    this.courseService.getCourses().subscribe(res => this.courses = res, err => console.error(err));
  }

  private initEventForm() {
    return new FormGroup({
      description: new FormControl(null, Validators.required),
      startTime: new FormControl(null, Validators.required),
      endTime: new FormControl(null, Validators.required),
      repeat: new FormControl(false),
      repeatTimes: new FormControl(0),
      repeatInterval: new FormControl('no'),
      eventTypeId: new FormControl(null, Validators.required),
      classRoomId: new FormControl(null, Validators.required),
      courseId: new FormControl(null)
    });
  }

  get description() { return this.eventForm.get('description'); }
  get startTime() { return this.eventForm.get('startTime'); }
  get endTime() { return this.eventForm.get('endTime'); }
  get repeat() { return this.eventForm.get('repeat'); }
  get repeatTimes() { return this.eventForm.get('repeatTimes'); }
  get repeatInterval() { return this.eventForm.get('repeatInterval'); }
  get eventTypeId() { return this.eventForm.get('eventTypeId'); }
  get classRoomId() { return this.eventForm.get('classRoomId'); }
  get courseId() { return this.eventForm.get('courseId'); }

}
