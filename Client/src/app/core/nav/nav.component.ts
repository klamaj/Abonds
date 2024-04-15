import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { logout } from 'src/app/modules/pages/auth/services/auth.actions';
import { AppState } from 'src/app/reducers';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {}

  logout(): void {
    this.store.dispatch(logout());
  }
}
