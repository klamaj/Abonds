import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { QuestionEntityService } from '../services/question-entity.service';
import { Observable } from 'rxjs';
import { QuestionCategory } from '../models/questionCategory.model';


@Component({
  selector: 'app-question-category',
  templateUrl: './question-category.component.html',
  styleUrls: ['./question-category.component.scss']
})
export class QuestionCategoryComponent implements OnInit {

  questions$: Observable<QuestionCategory[]> = new Observable<QuestionCategory[]>

  showForm: boolean = true;

  constructor(private questionService: QuestionEntityService) {
    
  }

  ngOnInit(): void {
    this.questions$ = this.questionService.entities$;
  }
}
