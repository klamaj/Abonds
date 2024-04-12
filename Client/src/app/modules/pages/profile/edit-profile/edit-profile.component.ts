import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Client } from '../../home/models/client.model';
import { ClientService } from '../../home/services/client.service';
import { ClientEntityService } from '../../home/services/client-entity.service';
import { Single } from '../../home/models/single.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  clientForm: FormGroup;
  notifyForm: FormGroup;
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
    public clientService: ClientService,
    public router: Router
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

    // notify client form
    this.notifyForm = new FormGroup({
      notifyUser: new FormControl(false, [Validators.required])
    })
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
    // console.log(this.clientForm.value);
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

  saveClient(save: boolean):void {

    // Create object
    let obj = {
      id: this.client!.id,
      firstName: this.clientForm.value.firstName,
      lastName: this.clientForm.value.lastName,
      dateOfBirth: this.client?.dateOfBirth,
      email: this.clientForm.value.email,
      sex: this.clientForm.value.sex,
      status: Number(this.clientForm.value.status),
      matchedUserId: Number(this.clientForm.value.matchedUserId),
      contractId: this.client?.contractId,
      contract: this.client?.contract,
      questionsSend: this.client?.questionsSend,
      answeredQuestions: this.client?.answeredQuestions
    }

    if(!save) {
      this.clientForm.patchValue({ ...this.client });
      this.clientForm.patchValue({
        status: this.client?.status.toString()
      });
      this.hasMatch = {
        id: 0,
        name: '',
        image: './assets/img/asset-1.png'
      }
      this.changesAlert = false;
    }
    if (this.notifyForm.value.notifyUser) {
      this.clientEntityService.update(obj).subscribe(
        res => {
          // console.log(res);
          this.clientService.sendMessage(this.client!.id, {message: res.toString()}).subscribe(
            rs => /*console.log(rs)*/ window.location.reload(),
            error => console.error(error)
          )
        },
        error => console.error(error)
      )
    }
    else {
      this.clientEntityService.update(obj).subscribe(
        res => /*console.log(res)*/ window.location.reload(),
        error => console.error(error)
      )
    }
  }
}