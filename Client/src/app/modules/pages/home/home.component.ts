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

  filtersForm: FormGroup;

  constructor(
  private fb: FormBuilder,
  private clientEntityService: ClientEntityService,
  public clientService: ClientService) {
    this.filtersForm = this.fb.group({
      search: new FormControl(''),
      gender: new FormControl('all'),
      minAge: new FormControl<number>(18),
      maxAge: new FormControl<number>(99),
      status: new FormControl('all')
    });
  }

  ngOnInit(): void {
    this.clients$ = this.clientEntityService.entities$;
  }

  
}
