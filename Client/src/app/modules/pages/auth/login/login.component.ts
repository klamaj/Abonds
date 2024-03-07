import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/reducers';
import { noop, tap } from 'rxjs';
import { login } from '../services/auth.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  type: string = 'password';

  constructor(
    private auth: AuthService,
    private router: Router,
    private store: Store<AppState>,
  ) {
    this.loginForm = this.generateLoginFormControl();
  }

  ngOnInit(): void { }

  // Login
  doLogin(): void {
    let obj = new Object({
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    });
    this.auth.login(obj)
      .pipe(
        tap( user => {
          this.store.dispatch(login({user}));

          this.router.navigateByUrl('/');
        })
      )
      .subscribe(
        noop,
        () => alert("Login failed")
      )
  }

  // Generate login Form
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
