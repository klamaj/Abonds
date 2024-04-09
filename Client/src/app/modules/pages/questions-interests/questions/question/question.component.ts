import { Component, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { QuestionEntityService } from '../services/question-entity.service';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionCategory } from '../models/questionCategory.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss']
})
export class QuestionComponent implements OnInit {

  question$: Observable<QuestionCategory | undefined> = new Observable<QuestionCategory>;

  answerFormDisp: boolean = false;
  answerForm: FormGroup;
  
  constructor(
    private questionService: QuestionEntityService,
    private route: ActivatedRoute,
    private router: Router
  ) {

    this.answerForm = new FormGroup({
      answerValue: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
    
    const QUESTION_ID = this.route.snapshot.paramMap.get('id');

    this.question$ = this.questionService.entities$
      .pipe(
        map(questions => questions.find(question => question.id.toString() == QUESTION_ID))
      );
  }

  showAnswerForm(): void {
    this.answerFormDisp = true;
  }

  addAnswer(): void {

  }

  removeCategory(id: number): void {
    this.questionService.delete(id).subscribe(
      res => {
        console.log(res);
        this.router.navigate(['/questions-interests']);
      }
    )
  }

}
