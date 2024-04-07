import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { InterestEntityService } from "./interest-entity.service";
import { filter, first, Observable, tap } from "rxjs";

@Injectable()
export class InterestsResolver implements Resolve<boolean> {

    constructor(private interestService: InterestEntityService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {

        return this.interestService.loaded$
            .pipe(
                tap(loaded => {
                    if (!loaded) {
                        this.interestService.getAll();
                    }
                }),
                filter(loaded => !!loaded),
                first()
            );
    }
}