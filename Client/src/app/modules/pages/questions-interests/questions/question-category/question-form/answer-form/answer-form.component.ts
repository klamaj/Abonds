import { Component, forwardRef, Input, OnDestroy } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { Answer, AnswerForm } from '../../models/answer.model';

@Component({
  selector: 'app-answer-form',
  templateUrl: './answer-form.component.html',
  styleUrls: ['./answer-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AnswerFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => AnswerFormComponent),
      multi: true
    }
  ]
})
export class AnswerFormComponent implements ControlValueAccessor, Validator, OnDestroy {

  @Input() type: any;

  destroySubject = new Subject<void>();

  // Answer Form
  answerForm = new FormGroup<AnswerForm>({
    value: new FormControl<string>("", Validators.required),
    questionId: new FormControl<number>(0)
  });

  // propagates value changes to parent form control when nested answer form changes
  registerOnChange(fn: any): void {
    this.answerForm.valueChanges
      .pipe(takeUntil(this.destroySubject))
      .subscribe(fn);
  }

  // marks parent form control as touched when nested answer form changes
  registerOnTouched(fn: any): void {
    this.answerForm.valueChanges
      .pipe(takeUntil(this.destroySubject))
      .subscribe(fn);
  }

  // disabled nested answer form when parent form control is disabled
  setDisabledState(isDisabled: boolean): void {
    isDisabled ? this.answerForm.disable() : this.answerForm.enable();
  }

  // writes value to nested answer form when value is set to parent form control
  writeValue(answer:  Answer): void {
    this.answerForm.patchValue(answer, { emitEvent: false });
  }

  // propagates validation errors from nested answer form to parent form control
  validate(control: AbstractControl<any, any>): ValidationErrors | null {
    return this.answerForm.valid ? null : { anwer: true };
  }

  // needed to unsubscribe from observables when answer component is destroyed
  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }
}
