import { FormArray, FormControl } from "@angular/forms";
import { Answer } from "./answer.model";

export interface Question {
    title: string;
    type: string;
    required: boolean;
    answers: Answer[];
}

export interface QuestionForm {
    title: FormControl<string | null>;
    type: FormControl<string | null>;
    required: FormControl<boolean | null>;
    answers: FormArray<FormControl<Answer | null>>;
}