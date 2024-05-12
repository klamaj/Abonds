import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-calendar-form',
  templateUrl: './calendar-form.component.html',
  styleUrls: ['./calendar-form.component.scss']
})

export class CalendarFormComponent implements OnInit {

  userRegistrationForm: FormGroup;

  constructor() {
    this.userRegistrationForm = new FormGroup({
      email: new FormControl<string>("", [Validators.required, Validators.email]),
      name: new FormControl<string>("", [Validators.required]),
      surname: new FormControl<string>("", [Validators.required]),
      birthday: new FormControl<string>("", [Validators.required])
    });
  }

  ngOnInit(): void {
    
  }

}
