import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { map, Observable } from 'rxjs';
import { Client } from './models/client.model';
import { ClientEntityService } from './services/client-entity.service';
import { ClientService } from './services/client.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  clients$: Client[] = [];

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
    this.search();
  }

  
  search(): void {
    let search = "?";
    if (this.filtersForm.value.search != '') {
      search = `${search}search=${this.filtersForm.value.search}`;
    }
    if (this.filtersForm.value.gender != 'all') {
      
      search = (search != '?') ? `${search}&gender=${this.filtersForm.value.gender}` : `${search}gender=${this.filtersForm.value.gender}`;
    }
    if (this.filtersForm.value.minAge != 18) {
      search = (search != '?') ? `${search}&ageFrom=${this.filtersForm.value.minAge}` :`${search}ageFrom=${this.filtersForm.value.minAge}`;
    }
    if (this.filtersForm.value.maxAge != 99) {
      search = (search != '?') ? `${search}&ageTo=${this.filtersForm.value.maxAge}` : `${search}ageTo=${this.filtersForm.value.maxAge}`;
    }
    if (this.filtersForm.value.status != 'all') {
      search = (search != '?') ? `${search}&status=${this.filtersForm.value.status}` : `${search}status=${this.filtersForm.value.status}`;
    }

    this.clientEntityService.getWithQuery(search).subscribe(
      res => this.clients$ = res
    );
  }
}
