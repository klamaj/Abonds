import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  contactForm: FormGroup;

  constructor() {
    this.contactForm = new FormGroup({
      email: new FormControl<string>("", [Validators.required, Validators.email]),
      name: new FormControl<string>("", [Validators.required]),
      message: new FormControl<string>("", [Validators.required])
    });
  }
  ngOnInit(): void {
      
  }
}
