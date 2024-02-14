import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmValidator } from './confirm-password.validator';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordForm: FormGroup;
  private token: string = "";
  email: string = "";

  constructor(private route: ActivatedRoute, private authService: AuthService) {
    this.email = route.snapshot.queryParams['email'];
    this.token = route.snapshot.queryParams['token'];
    this.resetPasswordForm = this.generateResetPasswordForm();
  }


  ngOnInit(): void {
      console.log(this.token);
  }

  resetPassword(): void {
    let obj = new Object({
      email: this.email,
      token: this.token,
      password: this.resetPasswordForm.value.password,
      confirmPassword: this.resetPasswordForm.value.confirmPassword
    });

    this.authService.resetPassword(obj).subscribe(
      res => console.log(res),
      err => console.error(err)
      // () => console.log("boom")     
    );
  }

  // Generate Password Form
  generateResetPasswordForm(): FormGroup {
    return new FormGroup({
      email: new FormControl({ value: this.email, disabled: true }, [Validators.required, Validators.email]),
      name: new FormControl({ value: 'Example Example', disabled: true }, [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])},
      // {
      //   validators: [ConfirmValidator.match('password', 'confirmPassword')]
      // }
    );
  }

}
