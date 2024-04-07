import { Injectable } from "@angular/core";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { Interest } from "../models/interest.model";

@Injectable()
export class InterestEntityService extends EntityCollectionServiceBase<Interest> {

    constructor(serviceElementsFactory: EntityCollectionServiceElementsFactory) {
        super('Interest', serviceElementsFactory);
    }
}