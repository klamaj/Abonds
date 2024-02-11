import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-user-form',
  templateUrl: './add-user-form.component.html',
  styleUrls: ['./add-user-form.component.scss']
})
export class AddUserFormComponent implements OnInit {

  addUserForm: FormGroup;

  private anio: number = new Date().getFullYear();  

  constructor() {
    this.addUserForm = this.generateAddUserForm();
  }

  ngOnInit(): void {}

  generateAddUserForm(): FormGroup {
    return new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      name: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z]')]),
      surname: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z]')]),
      day: new FormControl(undefined, [Validators.required, Validators.pattern('[0-9]'), Validators.min(1), Validators.max(31)]),
      month: new FormControl('00', [Validators.required, Validators.pattern('[0-9]')]),
      year: new FormControl(undefined, [Validators.required, Validators.pattern('[0-9]'), Validators.max(this.anio - 18)]),
      sex: new FormControl('male',[Validators.required]),
      questions: new FormControl('0', [Validators.required]),
      questionId: new FormControl('0')
    })
  }

  addUser(): void {
    console.log('boom')
  }
}
