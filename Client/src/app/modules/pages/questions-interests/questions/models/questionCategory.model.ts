import { Question } from "./question.model";

export interface QuestionCategory {
    questionCategoryTitle: string;
    id: number;
    questions: Question[];
}