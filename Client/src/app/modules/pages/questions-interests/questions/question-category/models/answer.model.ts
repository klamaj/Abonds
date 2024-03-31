import { FormControl } from "@angular/forms";

export interface Answer {
    value: string;
    questionId: number;
}

export interface AnswerForm {
    value: FormControl<string | null>;
    questionId: FormControl<number | null>;
}