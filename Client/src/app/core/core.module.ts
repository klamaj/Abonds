import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { AddUserComponent } from './add-user/add-user.component';
import { IconsModule } from '../shared/icons/icons.module';
import { AddUserFormComponent } from './add-user/add-user-form/add-user-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RequestsComponent } from './requests/requests.component';
import { RouterModule } from '@angular/router';
import { QuestionEntityService } from '../modules/pages/questions-interests/questions/services/question-entity.service';
import { QuestionDataService } from '../modules/pages/questions-interests/questions/services/questions-data.service';
import { ClientEntityService } from '../modules/pages/home/services/client-entity.service';
import { ClientsDataService } from '../modules/pages/home/services/clients-data.service';
import { EntityDataService, EntityDefinitionService, EntityMetadataMap } from '@ngrx/data';
import { CookiesComponent } from './cookies/cookies.component';
import { PrivacyComponent } from './privacy/privacy.component';
import { CookieService } from 'ngx-cookie-service';

// Entity metadata
const entityMetadata: EntityMetadataMap = {
  Interest: {},
  Question: {},
  Client: {}
};

@NgModule({
  declarations: [
    NavComponent,
    AddUserComponent,
    AddUserFormComponent,
    RequestsComponent,
    CookiesComponent,
    PrivacyComponent
  ],
  imports: [
    CommonModule,
    IconsModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  exports: [
    NavComponent,
    AddUserComponent,
    RequestsComponent,
    CookiesComponent
  ],
  providers: [
    QuestionEntityService,
    QuestionDataService,
    ClientEntityService,
    ClientsDataService,
    CookieService
  ]
})
export class CoreModule { 

  constructor(
    private eds: EntityDefinitionService,
    private entityDataService: EntityDataService,
    // private interestsService: InterestsDataService,
    private questionsService: QuestionDataService,
    private clientsService: ClientsDataService
  ) {

    eds.registerMetadataMap(entityMetadata);

    // entityDataService.registerService('Interest', interestsService);
    entityDataService.registerService('Question', questionsService);
    entityDataService.registerService('Client', clientsService);;
  }
}
