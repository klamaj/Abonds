import { Component, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { QuestionEntityService } from '../services/question-entity.service';
import { ActivatedRoute } from '@angular/router';
import { QuestionCategory } from '../models/questionCategory.model';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss']
})
export class QuestionComponent implements OnInit {

  question$: Observable<QuestionCategory | undefined> = new Observable<QuestionCategory>;
  
  constructor(
    private questionService: QuestionEntityService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    
    const QUESTION_ID = this.route.snapshot.paramMap.get('id');

    this.question$ = this.questionService.entities$
      .pipe(
        map(questions => questions.find(question => question.id.toString() == QUESTION_ID))
      );
  }
}
