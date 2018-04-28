import { AbstractControl } from '@angular/forms';

export class PasswordValidator {

  static matchPassword(group: AbstractControl) {
    const password: AbstractControl = group.get('password');
    const confirmPassword: AbstractControl = group.get('confirmPassword');

    if (password.value === confirmPassword.value) {
      return null;
    }
    return { matchPassword: true };
  }

}
