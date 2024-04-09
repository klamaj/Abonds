import { Injectable } from "@angular/core";
import { DefaultDataService, HttpUrlGenerator } from "@ngrx/data";
import { Client } from "../models/client.model";
import { HttpClient } from "@angular/common/http";
import { HttpOptions } from "@ngrx/data/src/dataservices/interfaces";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable()
export class ClientsDataService extends DefaultDataService<Client> {
    
    constructor(http: HttpClient, httpUrlGenerator: HttpUrlGenerator) {
        super('Client', http, httpUrlGenerator);
    }

    override getAll(options?: HttpOptions | undefined): Observable<Client[]> {
        return this.http.get<Client[]>(`${environment.apiUrl}/Clients`);
    }
}