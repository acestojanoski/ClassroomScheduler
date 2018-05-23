import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {PasswordValidator} from 'validators/password.validator';
import {UserService} from 'services/user.service';

@Component({
  selector: 'crs-password-manager',
  templateUrl: './password-manager.component.html',
  styleUrls: ['./password-manager.component.scss']
})
export class PasswordManagerComponent implements OnInit {

  public passwordForm: FormGroup;
  public errors: string[];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.passwordForm = this.initPasswordForm();
  }

  submit() {
    if (this.passwordForm.invalid) { return; }

    this.userService.changePassword(this.passwordForm.value).subscribe(res => {
      this.errors = null;
    }, err => {
      this.errors = err.error.errors ? err.error.errors.map(error => error.description) : err.error.Password;
    });
  }

  reset() {
    this.passwordForm.reset();
  }

  private initPasswordForm(): FormGroup {
    return new FormGroup({
      oldPassword: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      confirmPassword: new FormControl(null, Validators.required)
    }, { validators: PasswordValidator.matchPassword });
  }

  get oldPassword() { return this.passwordForm.get('oldPassword'); }
  get password() { return this.passwordForm.get('password'); }
  get confirmPassword() { return this.passwordForm.get('confirmPassword'); }
}
