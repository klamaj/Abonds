import { FormArray, FormControl } from "@angular/forms";
import { Question } from "./question.model";

export interface QuestionCategory {
    title: string;
    questions: Question[];
}

export interface QuestionCategoryForm {
    title: FormControl<string | null>;
    questions: FormArray<FormControl<Question | null>>;
}