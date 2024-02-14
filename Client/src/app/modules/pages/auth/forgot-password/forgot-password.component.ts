import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  forgotPasswordForm: FormGroup;
  showSuccess: boolean = false;

  constructor(private authService: AuthService) {
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
    let obj = new Object({
      email: this.forgotPasswordForm.value.email,
      clientUri: "https://localhost:4200/account/reset-password"
    });

    this.authService.forgotPassword(obj).subscribe(
      res => {
        this.showSuccess = true;
      },
      err => console.log(err),
      // () => console.log("fianlly")
    );
  }
}
