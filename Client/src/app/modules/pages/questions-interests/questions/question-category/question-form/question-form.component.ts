import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Question } from '../../models/question.model';
import { Answer } from '../../models/answer.model';
import { QuestionEntityService } from '../../services/question-entity.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.scss']
})
export class QuestionFormComponent implements OnInit {

  questionForm: FormGroup;
  singleQuestionForm: FormGroup;
  answerForm: FormGroup;

  questionDisp: boolean = true;
  answerDisp: boolean = false;

  questions: Question[] = new Array<Question>;
  answers: Answer[] = new Array<Answer>;

  constructor(private questionService: QuestionEntityService, private router: Router) {
    this.questionForm = new FormGroup({
      questionCategoryTitle: new FormControl('', [Validators.required])
    });

    // Single Question Form
    this.singleQuestionForm = new FormGroup({
      questionTitle: new FormControl('', [Validators.required]),
      questionType: new FormControl('', [Validators.required]),
      required: new FormControl(false)
    });

    this.answerForm = new FormGroup({
      answerValue: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
      
  }

  addQuestion(): void {
    let obj = {
      id: 0,
      questionCategoryTitle: this.questionForm.value.questionCategoryTitle,
      questions: this.questions
    }

    console.log(obj);

    this.questionService.add(obj).subscribe(
      res => {
        // console.log(res)
        this.router.navigateByUrl('/questions-interests');
      }
    )
  }

  displayQuestionForm(): void {
    this.questionDisp = true;
  }

  saveSingleQuestion(): void {
    let obj = {
      questionTitle: this.singleQuestionForm.value.questionTitle,
      questionType: this.singleQuestionForm.value.questionType,
      required: this.singleQuestionForm.value.required === null ? false  : true,
      questionCategoryId: 0,
      id: 0,
      questionAnswers: this.answers
    }
    this.questions.push(obj);
    // console.log(this.questions);

    this.answers = new Array<Answer>;
    
    this.singleQuestionForm.reset();
    this.questionDisp = false;
  }

  displayAnswerForm(): void {
    this.answerDisp = true;
  }

  saveAnswers(): void {
    let obj = {
      answerValue: this.answerForm.value.answerValue,
      questionId: 0,
      id: 0
    }

    this.answers.push(obj);
    console.log(this.answers);

    this.answerForm.reset();
    this.answerDisp = false;
  }
}
