import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientHomeComponent } from './client-home/client-home.component';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './client-home/header/header.component';
import { ContactComponent } from './client-home/contact/contact.component';

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
    ContactComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class WebModule { }
