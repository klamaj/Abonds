import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-questions-interests',
  templateUrl: './questions-interests.component.html',
  styleUrls: ['./questions-interests.component.scss']
})
export class QuestionsInterestsComponent implements OnInit {

  public pill: string = "questions";

  constructor() {}

  ngOnInit(): void { }

}
