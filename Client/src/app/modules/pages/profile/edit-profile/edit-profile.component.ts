import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Client } from '../../home/models/client.model';
import { ClientService } from '../../home/services/client.service';
import { ClientEntityService } from '../../home/services/client-entity.service';
import { Single } from '../../home/models/single.model';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  clientForm: FormGroup;
  deleteClientForm: FormGroup;
  singles$: Single[] = [];
  hasMatch = {
    id: 0,
    name: '',
    image: './assets/img/asset-1.png'
  }
  listDisp: boolean = false;
  changesAlert: boolean = false;
  showMessage: boolean = false;

  @Input() client: Client | undefined;

  constructor(
    private clientEntityService: ClientEntityService,
    public clientService: ClientService
  ) {

    // Client Form
    this.clientForm = new FormGroup({
      id: new FormControl("", [Validators.required]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl('', [Validators.required]),
      sex: new FormControl('', [Validators.required]),
      status: new FormControl('', [Validators.required]),
      matchedUserId: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email])
    });

    // DeleteClientForm
    this.deleteClientForm = new FormGroup({
      deleteClientMessage: new FormControl('', Validators.required)
    });
  }

  ngOnInit(): void {
    this.clientForm.patchValue({...this.client});
    this.clientForm.patchValue({
      status: this.client?.status.toString()
    })

    // Get Singles
    if(this.client?.sex == 'male') {
      this.clientService.getSingles('female').subscribe(
        singles => this.singles$ = singles
      )
    }
    else {
      this.clientService.getSingles('male').subscribe(
        singles => this.singles$ = singles
      )
    }
  }

  // Update Client
  updateClient(): void {
    console.log(this.clientForm.value);
    this.changesAlert = true;
  }

  findPersons(gender: string): void {
    
  }

  setMatch(item: Single): void{
    this.clientForm.value.matchedUserId = item.id;
    this.hasMatch.id = item.id;
    this.hasMatch.name = item.name;
    this.listDisp = false;
  }
}