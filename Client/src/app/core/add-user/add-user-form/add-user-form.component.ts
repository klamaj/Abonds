import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-user-form',
  templateUrl: './add-user-form.component.html',
  styleUrls: ['./add-user-form.component.scss']
})
export class AddUserFormComponent implements OnInit {

  addUserForm: FormGroup;

  constructor() {
    this.addUserForm = this.generateAddUserForm();
  }

  ngOnInit(): void {
      
  }

  generateAddUserForm(): FormGroup {
    return new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      name: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z]')]),
      surname: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z]')])
    })
  }

  addUser(): void {}
}
