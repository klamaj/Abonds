import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { QuestionEntityService } from "./question-entity.service";
import { filter, first, Observable, tap } from "rxjs";

@Injectable()
export class QuestionsResolver implements Resolve<boolean> {

    constructor(private questionService: QuestionEntityService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {

        return this.questionService.loaded$
            .pipe(
                tap(loaded => {
                    if(!loaded) {
                        this.questionService.getAll();
                    }
                }),
                filter(loaded => !!loaded),
                first()
            );
    }
}