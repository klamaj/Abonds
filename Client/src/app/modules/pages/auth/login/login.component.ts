import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  type: string = 'password';

  constructor(private fb: FormBuilder) {

    this.loginForm = this.generateLoginFormControl();
  }

  ngOnInit(): void {
      
  }

  // Login
  login(): void {
    
  }

  generateLoginFormControl(): FormGroup {
    return new FormGroup({
      email: new FormControl(undefined, [Validators.email, Validators.required]),
      password: new FormControl(undefined, [Validators.required])
    });
  }

  // Show and hide passwd
  showHidePasswd(): void {
    this.type = (this.type == 'password') ? 'text' : 'password';
  }
}
