import { Component, OnInit } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { initFlowbite } from 'flowbite';
import { Observable } from 'rxjs';
import { AppState } from './reducers';
import { select, Store } from '@ngrx/store';
import { login } from './modules/pages/auth/services/auth.actions';
import { isLoggedIn, isLoggedOut } from './modules/pages/auth/services/auth.selectors';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Client';

  loading = true;

  isLoggedIn$: Observable<boolean> | undefined;

  isLoggedOut$: Observable<boolean> | undefined;

  constructor(public router: Router,
      private store: Store<AppState>) {}

  ngOnInit(): void {
    initFlowbite();

    const userProfile = localStorage.getItem("user");

    if (userProfile) {
      this.store.dispatch(login({ user: JSON.parse(userProfile) }));
    }

    this.router.events.subscribe(event => {
      switch (true) {
        case event instanceof NavigationStart: {
          this.loading = true;
          break;
        }

        case event instanceof NavigationEnd:
        case event instanceof NavigationCancel:
        case event instanceof NavigationError: {
          this.loading = false;
          break;
        }
        default: {
          break;
        }
      }
    });

   

    this.isLoggedIn$ = this.store
      .pipe(
        select(isLoggedIn)
      );

    this.isLoggedOut$ = this.store
      .pipe(
        select(isLoggedOut)
      );

    // console.log(this.isLoggedIn$);
    // console.log(this.isLoggedOut$);
  }
}
