import { Component, OnInit } from '@angular/core';
import { GlobalDataService } from 'src/app/core/services/global-data.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-client-home',
  templateUrl: './client-home.component.html',
  styleUrls: ['./client-home.component.scss']
})
export class ClientHomeComponent implements OnInit{

  baseUrl = environment.baseApiUrl;

  public cookies: boolean | undefined;
  
  constructor(private globalData: GlobalDataService) {}

  ngOnInit(): void {
    this.globalData.cookies.subscribe( val => {
      this.cookies = val;
    });
  }
}
