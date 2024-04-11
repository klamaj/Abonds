import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Client } from './models/client.model';
import { ClientEntityService } from './services/client-entity.service';
import { ClientService } from './services/client.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  clients$: Observable<Client[] | undefined> = new Observable<Client[]>

  searchForm: FormGroup;

  constructor(
  private fb: FormBuilder,
  private clientService: ClientEntityService,
  public calcAge: ClientService) {
    this.searchForm = this.fb.group({
      search: new FormControl(''),
      gender: new FormControl('all'),
    });
  }

  ngOnInit(): void {
      this.clients$ = this.clientService.entities$;
  }

  search(): void{
    console.log(this.searchForm.value);
  }

  
}
