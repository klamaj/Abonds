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
import { EntityDataService, EntityDefinitionService, EntityMetadataMap } from '@ngrx/data';
import { InterestEntityService } from './questions-interests/interests/services/interest-entity.service';
import { InterestsDataService } from './questions-interests/interests/services/interests-data.service';
import { InterestsResolver } from './questions-interests/interests/services/interests.resolver';
import { ProfileComponent } from './profile/profile.component';
import { QuestionEntityService } from './questions-interests/questions/services/question-entity.service';
import { QuestionDataService } from './questions-interests/questions/services/questions-data.service';
import { QuestionsResolver } from './questions-interests/questions/services/questions.resolver';
import { QuestionFormComponent } from './questions-interests/questions/question-category/question-form/question-form.component';
import { QuestionComponent } from './questions-interests/questions/question/question.component';

export const pagesRoutes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'questions-interests',
    component: QuestionsInterestsComponent,
    resolve: {
      interest: InterestsResolver,
      question: QuestionsResolver
    }
  },
  {
    path: 'profile/:id',
    component: ProfileComponent
  },
  {
    path: 'add-question',
    component: QuestionFormComponent,
    resolve: {
      question: QuestionsResolver
    }
  },
  {
    path: 'question/:id',
    component: QuestionComponent,
    resolve: {
      question: QuestionsResolver
    }
  }
];

// Entity metadata
const entityMetadata: EntityMetadataMap = {
  Interest: {},
  Question: {}
};

@NgModule({
  declarations: [
    HomeComponent,
    QuestionsInterestsComponent,
    InterestsComponent,
    QuestionsComponent,
    QuestionCategoryComponent,
    ProfileComponent,
    QuestionFormComponent,
    QuestionComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(pagesRoutes),
    FormsModule,
    ReactiveFormsModule,
    CoreModule
  ],
  providers: [
    InterestEntityService,
    InterestsDataService,
    InterestsResolver,
    QuestionEntityService,
    QuestionDataService,
    QuestionsResolver
  ]
})

export class PagesModule {

  constructor(
    private eds: EntityDefinitionService,
    private entityDataService: EntityDataService,
    private interestsService: InterestsDataService,
    private questionsService: QuestionDataService
  ) {

    eds.registerMetadataMap(entityMetadata);

    entityDataService.registerService('Interest', interestsService);
    entityDataService.registerService('Question', questionsService);
  }
}
