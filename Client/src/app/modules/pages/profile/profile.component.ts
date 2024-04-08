import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  questionsForm: FormGroup;

  constructor() {
    this.questionsForm = this.generateQuestionForm();
  }

  generateQuestionForm(): FormGroup {
    return new FormGroup({
      sendQuest: new FormControl('0', [Validators.required])
    });
  }

  ngOnInit(): void {
      
  }

  sendQuestions(): void {

  }
}
