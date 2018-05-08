import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ClassRoomService } from 'services/class-room.service';
import { BuildingService } from 'services/building.service';

@Component({
  selector: 'crs-create-classroom',
  templateUrl: './create-classroom.component.html',
  styleUrls: ['./create-classroom.component.scss']
})
export class CreateClassroomComponent implements OnInit {

  @Output() public newClassroom: EventEmitter<any> = new EventEmitter<any>();
  public classroomForm: FormGroup;
  public opened: boolean = false;
  public classroomId: number;
  public buildings = [];
  public pick: any = [];
  public picklistOpen = false;

  constructor(
    private classroomService: ClassRoomService,
    private buildingService: BuildingService
  ) { }

  ngOnInit() {
    this.classroomForm = this.initClassroomForm();
    this.getBuildings();
  }

  public open(classroomId = null) {
    this.classroomId = classroomId;
    this.opened = true;

    if(this.classroomId){
      this.getClassroomById(this.classroomId);
    }
  }

  submit() {
    if (this.classroomForm.invalid || this.pick.length < 1) { return; }

    const classroom = Object.assign({}, this.classroomForm.value);
    classroom.buildingId = this.pick.id;
    console.log(this.pick);

    if (this.classroomId) {
      this.updateClassroom(this.classroomId, classroom);
    } else {
      this.createClassroom(classroom);
    }
  }

  private createClassroom(classroom) {
    this.classroomService.createClassRoom(classroom).subscribe(res =>{
      this.classroomForm.reset();
      this.pick = [];
      this.opened = false;
      this.newClassroom.emit();
    }, err => console.error(err));
  }

  private updateClassroom(id, classroom) {
    this.classroomService.updateClassRoom(id, classroom).subscribe(res => {
      this.classroomForm.reset();
      this.pick = [];
      this.opened = false;
      this.newClassroom.emit();
    }, err => console.error(err));
  }

  getClassroomById(id) {
    this.classroomService.getClassRoomById(id).subscribe(res => this.classroomForm.patchValue(res), err => console.error(err));
  }

  getBuildings() {
    this.buildingService.getBuildings().subscribe(res => {
      this.buildings = res.map(p => {
        const obj = {name: p.name, id: p.id};
        return obj;
      });
    }, err => console.error(err));
  }

  private initClassroomForm() {
    return new FormGroup({
      name: new FormControl(null, Validators.required)
    });
  }
  get pickLabel() {
    return this.pick.value || 'Select an option';
  }

  get name() { return this.classroomForm.get('name');}
}
