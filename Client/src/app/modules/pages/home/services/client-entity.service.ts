import { Injectable } from "@angular/core";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { Client } from "../models/client.model";

@Injectable()
export class ClientEntityService extends EntityCollectionServiceBase<Client> {

    constructor(serviceElementsFactory: EntityCollectionServiceElementsFactory) {
        super('Client', serviceElementsFactory);
    }
}