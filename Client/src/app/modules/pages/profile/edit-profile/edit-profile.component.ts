import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Client } from '../../home/models/client.model';
import { ClientService } from '../../home/services/client.service';
import { ClientEntityService } from '../../home/services/client-entity.service';
import { Single } from '../../home/models/single.model';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  baseUrl = environment.apiUrl;

  clientForm: FormGroup;
  matchedClientForm: FormGroup;
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
  devideAlert: boolean = false;

  @Input() client: Client | undefined;
  matchedClient: Client | undefined;

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

    // Define mathed form
    this.matchedClientForm = new FormGroup({
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
    // console.log(this.client!.matchedUser);
    // Values to ClientForm
    this.clientForm.patchValue({...this.client});
    this.clientForm.patchValue({
      status: this.client?.status.toString()
    });

    // Values to matchClientForm
    if (this.client?.matchedUserId != null) {
      this.clientService.getClientById(this.client?.matchedUserId).subscribe(
        client => {
          this.matchedClientForm.patchValue({ ...client });
          this.matchedClientForm.patchValue({
            status: client.status.toString()
          });
          this.matchedClient = client;
          this.hasMatch = {
            id: client.id,
            name: client.firstName + client.lastName,
            image: './assets/img/asset-1.png'
          }
        }
      )
    }

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

  setMatch(item: Single): void{
    this.clientForm.value.matchedUserId = item.id;
    this.hasMatch.id = item.id;
    this.hasMatch.name = item.name;
    this.listDisp = false;
  }

  saveClient(save: boolean):void {


    if(!save) {
      this.clientForm.patchValue({ ...this.client });
      this.clientForm.patchValue({
        status: this.client?.status.toString()
      });
      this.hasMatch = {
        id: 0,
        name: '',
        image: './assets/img/asset-1.png'
      };
      if (this.client?.matchedUserId != null) {
        this.matchedClientForm.patchValue({ ...this.matchedClient });
        this.matchedClientForm.patchValue({
          status: this.matchedClient?.status.toString()
        });
      }
      this.changesAlert = false;
    }
    else {
      if (this.notifyForm.value.notifyUser) {
        if (this.client!.matchedUserId != null) { this.updateMatchedUsers(true) } else  { this.updateSingleClient(true) }
      }
      else {
        if (this.client!.matchedUserId != null) { this.updateMatchedUsers(false) } else { this.updateSingleClient(false) }
      }
    }
  }

  devideProfile(devide: boolean):void {
    if(this.client?.matchedUserId != null && !devide) {
      this.devideAlert = true;
    }
    if(this.client?.matchedUserId != null && devide) {
      let objClient = {
        id: this.client!.id,
        firstName: this.clientForm.value.firstName,
        lastName: this.clientForm.value.lastName,
        dateOfBirth: this.client?.dateOfBirth,
        email: this.clientForm.value.email,
        sex: this.clientForm.value.sex,
        status: 0,
        matchedUserId: null,
        contractId: this.client?.contractId,
        contract: this.client?.contract,
        questionsSend: this.client?.questionsSend,
        answeredQuestions: this.client?.answeredQuestions
      }

      let objMatch = {
        id: this.matchedClient!.id,
        firstName: this.matchedClientForm.value.firstName,
        lastName: this.matchedClientForm.value.lastName,
        dateOfBirth: this.matchedClient!.dateOfBirth,
        email: this.matchedClientForm.value.email,
        sex: this.matchedClientForm.value.sex,
        status: 0,
        matchedUserId: null,
        contractId: this.matchedClient!.contractId,
        contract: this.matchedClient!.contract,
        questionsSend: this.matchedClient!.questionsSend,
        answeredQuestions: this.matchedClient!.answeredQuestions
      }

      this.clientEntityService.update(objClient).subscribe(
        res => {
          this.clientEntityService.update(objMatch).subscribe(
            res => {
              console.log(res);
              window.location.reload();
            },
            error => console.error(error)
          )
        },
        error => console.error(error)
      )
    }
  }

  keepAccounts(): void {
    this.clientForm.value.status = '1';
    this.devideAlert = false;
  }

  // Update mathced User Notification
  updateMatchedUsers(notify: boolean): void {
    // Create client object
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
    // Create Matched User Object
    let matchedObj = {
      id: this.matchedClient!.id,
      firstName: this.matchedClientForm.value.firstName,
      lastName: this.matchedClientForm.value.lastName,
      dateOfBirth: this.matchedClient!.dateOfBirth,
      email: this.matchedClientForm.value.email,
      sex: this.matchedClientForm.value.sex,
      status: 0,
      matchedUserId: this.matchedClient!.matchedUserId,
      contractId: this.matchedClient!.contractId,
      contract: this.matchedClient!.contract,
      questionsSend: this.matchedClient!.questionsSend,
      answeredQuestions: this.matchedClient!.answeredQuestions
    }
    
    if (notify){
      this.clientEntityService.update(obj).subscribe(
        res => {
          // console.log(res);
          this.clientEntityService.update(matchedObj).subscribe(
            res => {
              // console.log(res);
              this.clientService.sendMessage(obj.id, {message: `Updated client ${obj.id}`}).subscribe();
              this.clientService.sendMessage(matchedObj.id, { message: `Matched client ${matchedObj.id}`}).subscribe(
                res => window.location.reload()
              );
            }
          )
        }
      )
    }
    else {
      this.clientEntityService.update(obj).subscribe(
        res => {
          this.clientEntityService.update(matchedObj).subscribe(
            res => window.location.reload()
          )
        }
      )
    }
  }

  // Update Client
  updateSingleClient(notify: boolean): void {
    // Create client object
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

    if (notify) {
      this.clientEntityService.update(obj).subscribe(
        res => {
          this.clientService.sendMessage(obj.id, {message: res.toString()}).subscribe(
            res => window.location.reload()
          )
        }
      )
    }
    else {
      this.clientEntityService.update(obj).subscribe(
        res => window.location.reload()
      )
    }
  }
}