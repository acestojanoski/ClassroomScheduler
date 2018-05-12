import { Component, OnInit } from '@angular/core';

import 'rxjs/add/operator/finally';

import { EventType } from 'models/event-type.model';
import { EventTypeService } from 'services/event-type.service';

@Component({
  selector: 'crs-event-type-layout',
  templateUrl: './event-type-layout.component.html',
  styleUrls: ['./event-type-layout.component.scss']
})
export class EventTypeLayoutComponent implements OnInit {

  public eventTypes: EventType[] = [];
  public loading = true;

  constructor(private eventTypeService: EventTypeService) { }

  ngOnInit() {
    console.log(this.loading);
    this.getEventTypes();
  }

  public getEventTypes() {
    this.eventTypeService.getEventTypes()
      .finally(() => this.loading = false)
      .subscribe(res => {
        this.eventTypes = res;
    });
  }

}
