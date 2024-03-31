import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { initFlowbite } from 'flowbite';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Client';

  constructor(public router: Router) {}

  pageTitle = "";

  ngOnInit(): void {
    this.pageTitle = this.setPageTitle();
    initFlowbite();
  }

  setPageTitle(): string {
    if (this.router.url.includes("questions-interests")) {
      return "Questions & Interests";
    }
    else {
      return "Good morning Lila";
    }
  }
}
