import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  forgotPasswordForm: FormGroup;

  constructor() {
    this.forgotPasswordForm = this.generateForgotPasswordFormControl();
  }

  ngOnInit(): void {
      
  }

  generateForgotPasswordFormControl(): FormGroup {
    return new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }

  forgotPassword(): void {
    
  }
}
