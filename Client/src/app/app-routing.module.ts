import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/pages/auth/login/login.component';
import { RegisterComponent } from './modules/pages/auth/register/register.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>  import('./modules/pages/pages.module').then(m => m.PagesModule),
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
