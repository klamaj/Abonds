import { Injectable } from "@angular/core";
import { DefaultDataService, HttpUrlGenerator } from "@ngrx/data";
import { Image } from "../models/image.model";
import { HttpClient } from "@angular/common/http";
import { HttpOptions, QueryParams } from "@ngrx/data/src/dataservices/interfaces";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable()
export class ImagesDataService extends DefaultDataService<Image> {

    constructor(http: HttpClient, httpUrlGenerator: HttpUrlGenerator) {
        super('Image', http, httpUrlGenerator);
    }

    override getWithQuery(queryParams: string | QueryParams | undefined, options?: HttpOptions | undefined): Observable<Image[]> {
        return this.http.get<Image[]>(`${environment.apiUrl}/Clients/${queryParams}/Images`);
    }
}