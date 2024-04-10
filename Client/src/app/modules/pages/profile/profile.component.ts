import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { map, Observable } from 'rxjs';
import { Client } from '../home/models/client.model';
import { ActivatedRoute } from '@angular/router';
import { ClientEntityService } from '../home/services/client-entity.service';
import { ClientService } from '../home/services/client.service';
import { QuestionCategory } from '../questions-interests/questions/models/questionCategory.model';
import { QuestionEntityService } from '../questions-interests/questions/services/question-entity.service';
import { ObservableNotification } from '@ngrx/effects/src/utils';
import { Contract } from '../home/models/contract.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  client$: Observable<Client | undefined> = new Observable<Client>;
  questions$: QuestionCategory[] = [];

  displayQuestionList: boolean = false;

  questionsForm: FormGroup;
  sendMessageForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private clientEntityService: ClientEntityService,
    public clientService: ClientService,
    private questionsService: QuestionEntityService) {

    this.questionsForm = this.generateQuestionForm();

    // SendMessage Form
    this.sendMessageForm = new FormGroup({
      messageChek: new FormControl('', [Validators.required]),
      message: new FormControl('', [Validators.required])
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

    this.client$ = this.clientEntityService.entities$
      .pipe(
        map(clients => clients.find(client => client.id === Number(CLIENT_ID)))
      );

    this.questionsService.entities$.subscribe(
      res =>  {
        this.questions$ = res;
      }
    );
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
}
