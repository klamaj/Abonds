import { Injectable } from "@angular/core";
import { DefaultDataService, HttpUrlGenerator } from "@ngrx/data";
import { Client } from "../models/client.model";
import { HttpClient } from "@angular/common/http";
import { HttpOptions } from "@ngrx/data/src/dataservices/interfaces";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Update } from "@ngrx/entity";

@Injectable()
export class ClientsDataService extends DefaultDataService<Client> {
    
    constructor(http: HttpClient, httpUrlGenerator: HttpUrlGenerator) {
        super('Client', http, httpUrlGenerator);
    }

    override getAll(options?: HttpOptions | undefined): Observable<Client[]> {
        return this.http.get<Client[]>(`${environment.apiUrl}/Clients`);
    }

    override delete(key: number | string, options?: HttpOptions | undefined): Observable<string | number> {
        return this.http.delete<any>(`${environment.apiUrl}/Clients/${key}`);
    }

    override update(update: Update<any>, options?: HttpOptions | undefined): Observable<Client> {
        return this.http.put<Client>(`${environment.apiUrl}/Clients`, update.changes);
    }
}