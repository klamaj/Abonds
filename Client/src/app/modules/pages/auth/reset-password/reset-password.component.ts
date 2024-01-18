import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmValidator } from './confirm-password.validator';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordForm: FormGroup;

  constructor() {
    this.resetPasswordForm = this.generateResetPasswordForm();
  }


  ngOnInit(): void {
      
  }

  resetPassword(): void {}

  // Generate Password Form
  generateResetPasswordForm(): FormGroup {
    return new FormGroup({
      email: new FormControl({ value: 'example@example.com', disabled: true }, [Validators.required, Validators.email]),
      name: new FormControl({ value: 'Example Example', disabled: true }, [Validators.required]),
      newPassword: new FormControl('', [Validators.required])},
      {
        validators: [ConfirmValidator.match('password', 'confirmPassword')]
      });
  }

}
