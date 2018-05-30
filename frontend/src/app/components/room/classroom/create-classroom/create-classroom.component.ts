import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';

import {ClassRoomService} from 'services/class-room.service';
import {BuildingService} from 'services/building.service';

import {Building} from 'models/building.model';

@Component({
  selector: 'crs-create-classroom',
  templateUrl: './create-classroom.component.html',
  styleUrls: ['./create-classroom.component.scss']
})
export class CreateClassroomComponent implements OnInit {

  @Output() public newClassroom: EventEmitter<any> = new EventEmitter<any>();
  public classroomForm: FormGroup;
  public opened = false;
  public classroomId: number;
  public buildings: Building[] = [];
  public editing = false;

  constructor(private classroomService: ClassRoomService,
              private buildingService: BuildingService) {
  }

  ngOnInit() {
    this.classroomForm = this.initClassroomForm();
    this.getBuildings();
  }

  public open(classroomId = null) {
    this.classroomId = classroomId;
    this.opened = true;
    this.classroomForm.reset();
    this.editing = false;
    if (this.classroomId) {
      this.getClassroomById(this.classroomId);
      this.editing = true;
    }
  }

  public submit() {
    if (this.classroomForm.invalid) {
      return;
    }

    const classroom = Object.assign({}, this.classroomForm.value);

    if (this.classroomId) {
      this.updateClassroom(this.classroomId, classroom);
    } else {
      this.createClassroom(classroom);
    }
  }

  private createClassroom(classroom) {
    this.classroomService.createClassRoom(classroom).subscribe(res => {
      this.opened = false;
      this.newClassroom.emit();
    }, err => console.error(err));
  }

  private updateClassroom(id, classroom) {
    this.classroomService.updateClassRoom(id, classroom).subscribe(res => {
      this.opened = false;
      this.newClassroom.emit();
    }, err => console.error(err));
  }

  private deleteClassroom() {
    this.classroomService.deleteClassRoom(this.classroomId).subscribe(res => {
      console.log(res);
      this.opened = false;
    }, err => {
      // this.error = true;
      console.error(err);
    });
  }

  private getClassroomById(id) {
    this.classroomService.getClassRoomById(id).subscribe(res => {
      const classroom: any = Object.assign({}, res);
      classroom.buildingId = res.building.id;
      this.classroomForm.patchValue(classroom);
    }, err => console.error(err));
  }

  private getBuildings() {
    this.buildingService.getBuildings().subscribe(res => this.buildings = res, err => console.error(err));
  }

  private initClassroomForm() {
    return new FormGroup({
      name: new FormControl(null, Validators.required),
      buildingId: new FormControl(null, Validators.required)
    });
  }

  get name() {
    return this.classroomForm.get('name');
  }

  get buildingId() {
    return this.classroomForm.get('buildingId');
  }

}
