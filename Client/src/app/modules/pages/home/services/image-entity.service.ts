import { Injectable } from "@angular/core";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { Image } from "../models/image.model";

@Injectable()
export class ImageEntityService extends EntityCollectionServiceBase<Image>{

    constructor(serviceElementsFactory: EntityCollectionServiceElementsFactory) {
        super('Image', serviceElementsFactory);
    }
}