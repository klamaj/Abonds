import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-client-home',
  templateUrl: './client-home.component.html',
  styleUrls: ['./client-home.component.scss']
})
export class ClientHomeComponent implements OnInit{

  baseUrl = environment.baseApiUrl;
  
  constructor() {}

  ngOnInit(): void {
      
  }
}
