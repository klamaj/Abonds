import { Injectable } from "@angular/core";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { QuestionCategory } from "../models/questionCategory.model";

@Injectable()
export class QuestionEntityService extends EntityCollectionServiceBase<QuestionCategory> {

    constructor(serviceElementsFactory: EntityCollectionServiceElementsFactory) {
        super('Question', serviceElementsFactory);
    }
}