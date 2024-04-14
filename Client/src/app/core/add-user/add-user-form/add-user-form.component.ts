import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Client } from 'src/app/modules/pages/home/models/client.model';
import { ClientEntityService } from 'src/app/modules/pages/home/services/client-entity.service';
import { ClientService } from 'src/app/modules/pages/home/services/client.service';
import { QuestionCategory } from 'src/app/modules/pages/questions-interests/questions/models/questionCategory.model';
import { QuestionEntityService } from 'src/app/modules/pages/questions-interests/questions/services/question-entity.service';

@Component({
  selector: 'app-add-user-form',
  templateUrl: './add-user-form.component.html',
  styleUrls: ['./add-user-form.component.scss']
})
export class AddUserFormComponent implements OnInit {

  addUserForm: FormGroup;

  questions$: QuestionCategory[] = [];
  showForm: boolean = false;

  private anio: number = new Date().getFullYear();  

  constructor(private questionsService: QuestionEntityService, private clientEntityService: ClientEntityService, private clientService: ClientService) {
    this.addUserForm = this.generateAddUserForm();
  }

  ngOnInit(): void {
    this.questionsService.getAll().subscribe(
      res => this.questions$ = res
    );
  }

  generateAddUserForm(): FormGroup {
    return new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      day: new FormControl(undefined, [Validators.required, Validators.min(1), Validators.max(31)]),
      month: new FormControl('00', [Validators.required]),
      year: new FormControl(undefined, [Validators.required, Validators.max(this.anio - 18)]),
      sex: new FormControl('male',[Validators.required]),
      questions: new FormControl('0', [Validators.required]),
      questionId: new FormControl('0')
    })
  }

  addUser(): void {
    let obj = {
      firstName: this.addUserForm.value.firstName,
      lastName: this.addUserForm.value.lastName,
      email: this.addUserForm.value.email,
      dateOfBirth: this.addUserForm.value.year + "-" + this.addUserForm.value.month + "-" + this.addUserForm.value.day,
      sex: this.addUserForm.value.sex,
    }

    this.clientEntityService.add(obj as Client).subscribe(
      res => {
        if (this.addUserForm.value.questions != '0' && this.addUserForm.value.questionId !='0') {
          this.clientService.sendQuestions(res.id, Number(this.addUserForm.value.questionId)).subscribe(
            res => console.log(res)
          );
        }
        this.showForm = false;
      }
    )
  }

  showFormFunc(): void {
    this.showForm = true;
  }
}
