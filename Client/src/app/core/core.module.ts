import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { AddUserComponent } from './add-user/add-user.component';
import { IconsModule } from '../shared/icons/icons.module';
import { AddUserFormComponent } from './add-user/add-user-form/add-user-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    NavComponent,
    AddUserComponent,
    AddUserFormComponent
  ],
  imports: [
    CommonModule,
    IconsModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    NavComponent
  ]
})
export class CoreModule { }
