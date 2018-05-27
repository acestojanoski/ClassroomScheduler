import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from 'services/auth.service';

@Component({
  selector: 'crs-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  public error: string;

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.loginForm = this.initLoginForm();
  }

  @HostListener('document:keydown.enter')
  submit() {
    if (this.loginForm.invalid) { return; }

    this.authService.login(this.loginForm.value).subscribe(res => {
      this.authService.setSession(res.token);
      this.error = null;
      this.router.navigate(['events']);
    }, err => this.error = err.error);
  }

  reset() {
    this.loginForm.reset();
  }

  private initLoginForm(): FormGroup {
    return new FormGroup({
      email: new FormControl(null, { validators: [Validators.required, Validators.email] }),
      password: new FormControl(null, Validators.required)
    });
  }

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }

}
