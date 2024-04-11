export interface ClientQuestionCat {
    questionCategory: string;
    questions: ClientQuestion[];
}

export interface ClientQuestion {
    questionTitle: string;
    questionType: string;
    answers: ClientAnswers[];
}

export interface ClientAnswers {
    answerValue: string;
    selected: boolean;
}