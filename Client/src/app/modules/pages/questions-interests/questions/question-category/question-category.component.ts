import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { QuestionCategoryForm } from './models/question-category.model';
import { Question } from './models/question.model';

@Component({
  selector: 'app-question-category',
  templateUrl: './question-category.component.html',
  styleUrls: ['./question-category.component.scss']
})
export class QuestionCategoryComponent implements OnInit {

  @Input() questionID: any;

  categoryQuestionForm = new FormGroup<QuestionCategoryForm>({
    title: new FormControl<string>(""),
    questions: new FormArray<FormControl<Question | null>>([])
  });

  constructor() {
    
  }

  ngOnInit(): void {}
}
