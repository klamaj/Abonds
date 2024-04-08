import { Injectable } from "@angular/core";
import { DefaultDataService, HttpUrlGenerator } from "@ngrx/data";
import { QuestionCategory } from "../models/questionCategory.model";
import { HttpClient } from "@angular/common/http";
import { HttpOptions } from "@ngrx/data/src/dataservices/interfaces";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable()
export class QuestionDataService extends DefaultDataService<QuestionCategory> {
    
    constructor(http: HttpClient, httpUrlGenerator: HttpUrlGenerator) {
        super('Question', http, httpUrlGenerator);
    }

    override getAll(options?: HttpOptions | undefined): Observable<QuestionCategory[]> {
        return this.http.get<QuestionCategory[]>(`${environment.apiUrl}/QuestionCategories`);
    }

    override add(entity: QuestionCategory, options?: HttpOptions | undefined): Observable<QuestionCategory> {
        console.log("Entity", entity);
        return this.http.post<QuestionCategory>(`${environment.apiUrl}/QuestionCategories`, entity);
    }
}