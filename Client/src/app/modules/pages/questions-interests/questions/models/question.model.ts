import { Answer } from "./answer.model";

export interface Question {
    questionTitle: string;
    questionType: string;
    required: boolean;
    questionCategoryId: number;
    id: number;
    questionAnswers: Answer[];
}