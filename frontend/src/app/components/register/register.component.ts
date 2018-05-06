import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { UserTypeService } from 'services/user-type.service';
import { UserService } from 'services/user.service';

import { PasswordValidator } from 'validators/password.validator';

import { UserType } from 'models/user-type.model';

@Component({
  selector: 'crs-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerForm: FormGroup;
  public userTypes: UserType[];
  public errors: string[];

  constructor(
    private userTypeService: UserTypeService,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.registerForm = this.initRegisterForm();
    this.getUserTypes();
  }

  submit() {
    if (this.registerForm.invalid) { return; }

    this.userService.register(this.registerForm.value).subscribe(res => {
      this.errors = null;
    }, err => {
      this.errors = err.error.errors ? err.error.errors.map(error => error.description) : err.error.Password
    });
  }

  reset() {
    this.registerForm.reset();
  }

  getUserTypes() {
    this.userTypeService.getUserTypes().subscribe(userTypes => this.userTypes = userTypes, err => console.error(err));
  }

  private initRegisterForm(): FormGroup {
    return new FormGroup({
      userName: new FormControl(null, Validators.required),
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      email: new FormControl(null, { validators: [Validators.required, Validators.email] }),
      password: new FormControl(null, Validators.required),
      confirmPassword: new FormControl(null, Validators.required),
      userType: new FormControl(null, Validators.required)
    }, { validators: PasswordValidator.matchPassword });
  }

  get userName() { return this.registerForm.get('userName'); }
  get firstName() { return this.registerForm.get('firstName'); }
  get lastName() { return this.registerForm.get('lastName'); }
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }
  get confirmPassword() { return this.registerForm.get('confirmPassword'); }
  get userTypeName() { return this.registerForm.get('userType'); }
}
