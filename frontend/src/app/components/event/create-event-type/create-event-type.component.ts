import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { EventTypeService } from 'services/event-type.service';

@Component({
  selector: 'crs-create-event-type',
  templateUrl: './create-event-type.component.html',
  styleUrls: ['./create-event-type.component.scss']
})
export class CreateEventTypeComponent implements OnInit {

  @Output() public newEventType: EventEmitter<any> = new EventEmitter<any>();
  public eventTypeForm: FormGroup;
  public opened = false;
  public eventTypeId: number;

  constructor(private eventTypeService: EventTypeService) { }

  ngOnInit() {
    this.eventTypeForm = this.initEventTypeForm();
  }

  public open(eventTypeId = null) {
    this.eventTypeId = eventTypeId;
    this.eventTypeForm.reset();
    this.opened = true;
    if (this.eventTypeId) {
      this.getEventTypeById(this.eventTypeId);
    }
  }

  public submit() {
    if (this.eventTypeForm.invalid) { return; }

    const eventType = Object.assign({}, this.eventTypeForm.value);

    if (this.eventTypeId) {
      this.updateEventType(this.eventTypeId, eventType);
    } else {
      this.createBuilding(eventType);
    }
  }

  private createBuilding(eventType) {
    this.eventTypeService.createEventType(eventType).subscribe(res => {
      this.opened = false;
      this.newEventType.emit();
    });
  }

  private updateEventType(id, eventType) {
    this.eventTypeService.updateEventType(id, eventType).subscribe(res => {
      this.opened = false;
      this.newEventType.emit();
    });
  }

  private getEventTypeById(id) {
    this.eventTypeService.getEventTypeById(id).subscribe(res => this.eventTypeForm.patchValue(res), err => console.error(err));
  }

  private initEventTypeForm() {
    return new FormGroup({
      name: new FormControl(null, Validators.required)
    });
  }

  get name() { return this.eventTypeForm.get('name'); }

}
