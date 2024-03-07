import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Store, select } from "@ngrx/store";
import { Observable, tap } from "rxjs";
import { AppState } from "src/app/reducers";
import { isLoggedIn } from "./auth.selectors";

@Injectable()
export class AuthGuard implements CanActivate {
    
    constructor(
        private store: Store<AppState>,
        private router: Router
    ) {

    }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> {

            return this.store
                .pipe(
                    select(isLoggedIn),
                    tap(loggedIn => {
                        if (!loggedIn) {
                            this.router.navigateByUrl('/accounts/login');
                        }
                    })
                )
    }
}