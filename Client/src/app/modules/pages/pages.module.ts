import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from 'src/app/core/core.module';
import { QuestionsInterestsComponent } from './questions-interests/questions-interests.component';
import { InterestsComponent } from './questions-interests/interests/interests.component';
import { QuestionsComponent } from './questions-interests/questions/questions.component';
import { QuestionCategoryComponent } from './questions-interests/questions/question-category/question-category.component';
import { QuestionFormComponent } from './questions-interests/questions/question-category/question-form/question-form.component';
import { AnswerFormComponent } from './questions-interests/questions/question-category/question-form/answer-form/answer-form.component';

export const pagesRoutes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'questions-interests',
    component: QuestionsInterestsComponent
  }
];

@NgModule({
  declarations: [
    HomeComponent,
    QuestionsInterestsComponent,
    InterestsComponent,
    QuestionsComponent,
    QuestionCategoryComponent,
    QuestionFormComponent,
    AnswerFormComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(pagesRoutes),
    FormsModule,
    ReactiveFormsModule,
    CoreModule
  ]
})
export class PagesModule { }
