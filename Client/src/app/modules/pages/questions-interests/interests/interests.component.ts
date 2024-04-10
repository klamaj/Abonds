import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Interest } from './models/interest.model';
import { InterestEntityService } from './services/interest-entity.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SubInterest } from './models/sub-interest.model';

@Component({
  selector: 'app-interests',
  templateUrl: './interests.component.html',
  styleUrls: ['./interests.component.scss']
})
export class InterestsComponent implements OnInit {

  interests$: Observable<Interest[] | undefined> = new Observable<Interest[]>;

  newIntDisp: boolean = false;
  updateIntDisp: boolean = false;
  subIntAddDisp: boolean = false;

  newInterestForm: FormGroup;
  updateInterestForm: FormGroup;
  subInterestForm: FormGroup;
  
  constructor(private interestsService: InterestEntityService) {

    // InterestForm
    this.newInterestForm = new FormGroup({
      interestName: new FormControl<string | any>('', [Validators.required]),
      interestColor: new FormControl<string | any>('', [Validators.required])
    });

    // UpdateInterestForm
    this.updateInterestForm = new FormGroup({
      interestName: new FormControl<string | any>('', [Validators.required]),
      interestColor: new FormControl<string | any>('', [Validators.required]),
      id: new FormControl<number | any>(0, [Validators.required])
    });

    // SubInterestForm
    this.subInterestForm = new FormGroup({
      subInterestName: new FormControl('', [Validators.required]),
      id: new FormControl(0, [Validators.required]),
      interestName: new FormControl<string | any>('', [Validators.required]),
      interestColor: new FormControl<string | any>('', [Validators.required])
    });
  }

  ngOnInit(): void {
    this.interests$ = this.interestsService.entities$;
  }

  newInterest(): void {
    this.interestsService.add(this.newInterestForm.value).subscribe(
      result => {
        this.newIntDisp = false;
        this.newInterestForm.reset();
      },  
      error => {
        console.error(error);
      }
    );
  }

  showInterestForm(entity: Interest): void {
    this.updateInterestForm.patchValue({...entity});
    this.updateIntDisp = true;
  }

  updateInterest(): void {
    console.log('UpdateForm', this.updateInterestForm.value);
    this.interestsService.update(this.updateInterestForm.value).subscribe(
      result => {
        console.log(result);
        this.updateIntDisp = false;
        this.updateInterestForm.reset()
      },
      error => {
        console.error(error);
      }
    )
  }

  deleteInterest(id: number): void {
    this.interestsService.delete(id).subscribe(
      result => {
        console.log(result)
      }
    )
  }

  showSubInt(entity: Interest): void {
    this.subInterestForm.patchValue({...entity});
    this.subInterestForm.value.interestId = entity.id;
    console.log(this.subInterestForm.value);
    this.subIntAddDisp = true;
  }

  addSubInterest():void {
    let sub: SubInterest = {
      subInterestName: this.subInterestForm.value.subInterestName,
      interestId: this.subInterestForm.value.id,
      id: 0
    };

    let obj = {
      id: this.subInterestForm.value.id,
      interestName: this.subInterestForm.value.interestName,
      interestColor: this.subInterestForm.value.interestColor,
      subInterests: [sub]
    }

    console.log(obj);
    this.interestsService.update(obj).subscribe(
      res => {
        console.log(res);
        this.subIntAddDisp = false;
        this.subInterestForm.reset();
      }
    )
  }
}
