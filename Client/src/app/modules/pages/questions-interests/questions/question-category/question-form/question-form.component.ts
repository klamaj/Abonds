import { Component, forwardRef, OnDestroy } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormArray, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { Question, QuestionForm } from '../models/question.model';
import { Answer } from '../models/answer.model';

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => QuestionFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting:forwardRef(() => QuestionFormComponent),
      multi: true
    }
  ]
})
export class QuestionFormComponent implements ControlValueAccessor, Validator, OnDestroy {

  destroySubject = new Subject<void>();

  // Question Form
  questionForm = new FormGroup<QuestionForm>({
    title: new FormControl<string>('', Validators.required),
    type: new FormControl<string>('', Validators.required),
    required: new FormControl<boolean>(false, Validators.required),
    answers: new FormArray<FormControl<Answer | null>>([])
  });

  // propagates value changes to parent form control when nested question form changes
  registerOnChange(fn: any): void {
      this.questionForm.valueChanges
        .pipe(takeUntil(this.destroySubject))
        .subscribe(fn);
  }

  // marks parent form control as touched when nested question form changes
  registerOnTouched(fn: any): void {
      this.questionForm.valueChanges
        .pipe(takeUntil(this.destroySubject))
        .subscribe(fn);
  }

  // disabled nested question form when parent form control is disabled
  setDisabledState(isDisabled: boolean): void {
    isDisabled ? this.questionForm.disable() : this.questionForm.enable();
  }

  // writes value to nested question form when value is set to parent form control
  writeValue(question: Question): void {
    this.questionForm.patchValue(question, { emitEvent: false });
  }

  // propagates validation errors from nested question form to parent form control
  validate(control: AbstractControl<any, any>): ValidationErrors | null {
    return this.questionForm.valid ? null : {question: true};
  }

  // needed to unsubscribe from observables when question component is destroyed
  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }
}
