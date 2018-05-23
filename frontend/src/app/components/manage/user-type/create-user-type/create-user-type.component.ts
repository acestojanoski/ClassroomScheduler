import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UserTypeService} from 'services/user-type.service';

@Component({
  selector: 'crs-create-user-type',
  templateUrl: './create-user-type.component.html',
  styleUrls: ['./create-user-type.component.scss']
})
export class CreateUserTypeComponent implements OnInit {

  @Output() public newUserType: EventEmitter<any> = new EventEmitter<any>();
  public userTypeForm: FormGroup;
  public opened = false;
  public userTypeId: number;


  constructor(private userTypeService: UserTypeService) {
  }

  ngOnInit() {
    this.userTypeForm = this.initUserTypeForm();
  }

  public open(userTypeId = null) {
    this.userTypeId = userTypeId;
    this.userTypeForm.reset();
    this.opened = true;
    if (this.userTypeId) {
      this.getUserTypeById(this.userTypeId);
    }
  }

  submit() {
    if (this.userTypeForm.invalid) {
      return;
    }

    const userType = Object.assign({}, this.userTypeForm.value);

    if (this.userTypeId) {
      this.updateUserType(this.userTypeId, userType);
    } else {
      this.createUserType(userType);
    }
  }

  private createUserType(userType) {
    this.userTypeService.createUserType(userType).subscribe(res => {
      this.opened = false;
      this.newUserType.emit();
    }, err => console.error(err));
  }

  private updateUserType(id, userType) {
    this.userTypeService.updateUserType(id, userType).subscribe(res => {
      this.opened = false;
      this.newUserType.emit();
    }, err => console.error(err));
  }

  getUserTypeById(id) {
    this.userTypeService.getUserTypeById(id).subscribe(res => this.userTypeForm.patchValue(res), err => console.error(err));
  }

  private initUserTypeForm() {
    return new FormGroup({
      name: new FormControl(null, Validators.required)
    });
  }

  get name() {
    return this.userTypeForm.get('name');
  }
}
