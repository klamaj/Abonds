import { Component, OnInit } from '@angular/core';
import { GlobalDataService } from '../services/global-data.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-cookies',
  templateUrl: './cookies.component.html',
  styleUrls: ['./cookies.component.scss']
})
export class CookiesComponent implements OnInit {

  public cookies: boolean | undefined;

  constructor(
    private globalData: GlobalDataService,
    private cookieService: CookieService
  ) {}

  ngOnInit(): void {
    this.globalData.cookies.subscribe( val => {
      this.cookies = val;
    })
  }

  accept(): void {
    this.cookieService.set('abonds-accept-cookies', 'true', 365);
    this.globalData.dispCookies(false);
  }

  decline(): void {
    this.globalData.dispCookies(false);
  }
}
