import { Component, ChangeDetectionStrategy, ViewChild, OnInit } from '@angular/core';
import { startOfDay, endOfDay, subDays, addDays, endOfMonth, isSameDay, isSameMonth, addHours } from 'date-fns';

import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/finally';

import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent } from 'angular-calendar';

import { EventService } from 'services/event.service';
import { CreateEventComponent } from '../create-event/create-event.component';
import { AuthService } from 'services/auth.service';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};

@Component({
  selector: 'crs-event-layout',
  templateUrl: './event-layout.component.html',
  styleUrls: ['./event-layout.component.scss']
})
export class EventLayoutComponent implements OnInit {

  @ViewChild('createEvent') createEvent: CreateEventComponent;
  loading = true;
  view = 'month';
  viewDate: Date = new Date();
  refresh: Subject<any> = new Subject();
  events: CalendarEvent[] = [];
  activeDayIsOpen = false;
  public loggedIn: boolean;

  constructor(
    private eventService: EventService,
    private auth: AuthService
  ) {}

  ngOnInit() {
    this.auth.user.subscribe(res => this.loggedIn = !!res);
    this.getEvents();
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
        this.viewDate = date;
      }
    }
  }

  handleEvent(action: string, event: CalendarEvent): void {
    if (this.loggedIn) {
      this.createEvent.open(event.id);
    }
  }

  public getEvents() {
    this.loading = true;
    this.eventService.getEvents()
      .finally(() => this.loading = false)
      .subscribe(res => {
        // TODO: FIX +/- 2 HOURS
        this.events = res.map(ev => {
          const event: CalendarEvent = {
            id: ev.id,
            start: new Date(ev.startTime),
            end: new Date(ev.endTime),
            title: `${ev.description} (${ev.eventType.name}) # Room: ${ev.classRoom.name} ${ev.course ? '#Course: ' + ev.course.name : ''}`
          };
          return event;
        });
        this.refresh.next();
    }, err => console.error(err));
  }

}
