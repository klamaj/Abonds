import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientHomeComponent } from './client-home/client-home.component';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './client-home/header/header.component';
import { ContactComponent } from './client-home/contact/contact.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FooterComponent } from './client-home/footer/footer.component';
import { SocialComponent } from './client-home/footer/social/social.component';
import { CalendarComponent } from './client-home/calendar/calendar.component';
import { NgbDatepicker, NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { CalendarFormComponent } from './client-home/calendar/calendar-form/calendar-form.component';

export const routes: Routes = [
  {
    path: '',
    component: ClientHomeComponent
  }
]

@NgModule({
  declarations: [
    ClientHomeComponent,
    HeaderComponent,
    ContactComponent,
    FooterComponent,
    SocialComponent,
    CalendarComponent,
    CalendarFormComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FormsModule,
    NgbDatepickerModule
  ]
})
export class WebModule { }
