import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { ClientEntityService } from "./client-entity.service";
import { filter, first, Observable, tap } from "rxjs";

@Injectable()
export class ClientsResolver implements Resolve<boolean> {
    
    constructor(private clientService: ClientEntityService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {

        return this.clientService.loaded$
            .pipe(
                tap(loaded => {
                    if (!loaded) {
                        this.clientService.getAll();
                    }
                }),
                filter(loaded => !!loaded),
                first()
            );
    }
}