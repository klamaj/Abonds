import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { map, Observable } from 'rxjs';
import { Client } from '../home/models/client.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientEntityService } from '../home/services/client-entity.service';
import { ClientService } from '../home/services/client.service';
import { QuestionCategory } from '../questions-interests/questions/models/questionCategory.model';
import { QuestionEntityService } from '../questions-interests/questions/services/question-entity.service';
import { ObservableNotification } from '@ngrx/effects/src/utils';
import { Contract } from '../home/models/contract.model';
import { ClientQuestionCat } from '../home/models/client-answers.model';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { Image } from '../home/models/image.model';
import { ImageEntityService } from '../home/services/image-entity.service';
import { ClientInterest } from '../home/models/client-interests.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  baseUrl = environment.apiUrl;

  client$: Observable<Client | undefined> = new Observable<Client>;
  questions$: QuestionCategory[] = [];
  clientAnswers$: ClientQuestionCat[] = [];
  partnerAnswers$: ClientQuestionCat[] = [];
  clientImages$: Image[] = [];
  partnerImages$: Image[] = [];
  clientInterests: ClientInterest[] = [];
  partnerInterests: ClientInterest[] = [];

  @ViewChild(EditProfileComponent) child: EditProfileComponent | undefined;

  displayQuestionList: boolean = false;
  editClient: boolean = false;
  deleteAlert: boolean = false;
  deleteClientDispForm: boolean = false;

  questionsForm: FormGroup;
  sendMessageForm: FormGroup;
  deleteClientForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private clientEntityService: ClientEntityService,
    private imageEntityService: ImageEntityService,
    public clientService: ClientService,
    private questionsService: QuestionEntityService,
    private router: Router) {

    this.questionsForm = this.generateQuestionForm();

    // SendMessage Form
    this.sendMessageForm = new FormGroup({
      messageChek: new FormControl('', [Validators.required]),
      message: new FormControl('', [Validators.required])
    });

    // Delete Client Form
    this.deleteClientForm = new FormGroup({
      deleteClientMessage: new FormControl('', Validators.required)
    });
  }

  generateQuestionForm(): FormGroup {
    return new FormGroup({
      questionId: new FormControl('0', [Validators.required])
    });
  }

  ngOnInit(): void {

    // Acitvated Route get: CLIENT_ID
    const CLIENT_ID = this.route.snapshot.paramMap.get('id');

    // Get Clients
    this.client$ = this.clientEntityService.entities$.pipe(
      map(clients => clients.find(client => client.id === Number(CLIENT_ID)))
    );
    
    // Get Client Answers
    if(this.client$.pipe(map(client => client?.answeredQuestions == true))) {
      this.clientService.getAnswers(CLIENT_ID).subscribe(
        answers => {
          // console.log('Client',answers);
          this.clientAnswers$ = answers;
        }
      );
    }

    this.clientService.getInterest(Number(CLIENT_ID)).subscribe(
      res => this.clientInterests = res
    );

    // Partner Answers
    this.client$.subscribe(
      res => {
        if(res?.matchedUserId != null) {
          this.clientService.getAnswers(res?.matchedUserId).subscribe(
            answers => {
              // console.log('Partner Answers', answers)
              this.partnerAnswers$ = answers;
              this.imageEntityService.getWithQuery(res.matchedUserId!.toString()).subscribe(
                res => this.partnerImages$ = res
              )
            });
          this.clientService.getInterest(res.matchedUserId).subscribe(
            res => this.partnerInterests = res
          )
        }
      }
    )

    this.imageEntityService.getWithQuery(CLIENT_ID!.toString()).subscribe(
      res => this.clientImages$ = res
    );

    // Get Questions
    if(this.client$.pipe(map(client => client?.questionsSend == false))) {
      this.questionsService.entities$.subscribe(
        res => {
          this.questions$ = res;
        }
      );
    }
  }

  sendMessage(clientId: number): void {
    let obj = {
      message: this.sendMessageForm.value.message
    }
    // console.log(obj);

    this.clientService.sendMessage(clientId, obj).subscribe(
      res => console.log(res)
    )
  }

  // Linear Helper
  numSequesnce(n: number): Array<number> {
    return Array(n);
  }

  // Update Client
  updateClient(): void {
    this.child!.updateClient();
  }

  // Delete client Alert
  deleteClient(notify: boolean, clientId: number): void {
    if (!notify) {
      this.clientEntityService.delete(clientId).subscribe(
        res => {
          this.deleteClientDispForm = true;
          this.router.navigateByUrl('/dashboard');
        },
        error => console.error(error)
      )
    }
    else {
      if(this.deleteClientDispForm) {
        let obj = {
          message: this.deleteClientForm.value.deleteClientMessage
        }
        this.clientService.sendMessage(clientId, obj).subscribe(
          res => {
            this.clientEntityService.delete(clientId).subscribe(
              res => this.router.navigateByUrl('/dashboard'),
              error => console.error(error)
            )
          },
          error => console.error(error)
        )
      }
      else {
        this.deleteClientDispForm = true;
      }
    }
  }

  // Upload Contrat
  onFileSelected(event: any, id: number): void {
    const file: File = event.target.files[0];

    this.clientService.uploadContractToClient(file, id).subscribe(
      res => console.log(res)
    )
  }

  // Send Questions
  sendQuestions(id: number): void {
    this.clientService.sendQuestions(id, Number(this.questionsForm.value.questionId)).subscribe(
      res => window.location.reload()
    )
  }
}
