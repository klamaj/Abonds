import { Injectable } from "@angular/core";
import { DefaultDataService, HttpUrlGenerator } from "@ngrx/data";
import { Interest } from "../models/interest.model";
import { HttpClient } from "@angular/common/http";
import { HttpOptions } from "@ngrx/data/src/dataservices/interfaces";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Update } from "@ngrx/entity";
import { SubInterest } from "../models/sub-interest.model";

@Injectable()
export class InterestsDataService extends DefaultDataService<Interest> {

    constructor(http: HttpClient, httpUrlGenerator: HttpUrlGenerator) {
        super('Interest', http, httpUrlGenerator);
    }

    override getAll(options?: HttpOptions | undefined): Observable<Interest[]> {
        return this.http.get<Interest[]>(`${environment.apiUrl}/Interests`);
    }

    override add(entity: Interest, options?: HttpOptions | undefined): Observable<Interest> {
        return this.http.post<Interest>(`${environment.apiUrl}/Interests`, entity);
    }

    override update(update: Update<Interest>, options?: HttpOptions | undefined): Observable<Interest> {
        console.log(update.changes);
        return this.http.put<Interest>(`${environment.apiUrl}/Interests`, update.changes);
    }

    override delete(key: string | number, options?: HttpOptions | undefined): Observable<string | number> {
        return this.http.delete<any>(`${environment.apiUrl}/Interests/${key}`);
    }

    public addSubInterest(update: Update<SubInterest>, options?: HttpOptions | undefined): Observable<Interest> {
        return this.http.put<Interest>(`${environment.apiUrl}/Interests/${update.changes.interestId}/SubInterests`, update.changes)
    }

    
}