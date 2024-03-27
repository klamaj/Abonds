import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent implements OnInit{

  rejectForm: FormGroup;

  constructor() {
    this.rejectForm = this.generateRejectForm();
  }

  ngOnInit(): void {
      
  }

  generateRejectForm(): FormGroup {
    return new FormGroup({
      message: new FormControl(undefined, [Validators.required]),
      status: new FormControl()
    });
  }

  rejectUser(): void {

  }
}
