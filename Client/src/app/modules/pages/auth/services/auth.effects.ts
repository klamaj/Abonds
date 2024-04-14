import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { AuthActions } from "./action-types";
import { tap } from "rxjs";

@Injectable()
export class AuthEffects {

    // login
    login$ = createEffect(() => 
        this.actions$
            .pipe(
                ofType(AuthActions.login),
                tap(action => localStorage.setItem('user', JSON.stringify(action.user)))
            ),
            { dispatch: false }
    );

    // logout
    logout$ = createEffect(() =>
        this.actions$
            .pipe(
                ofType(AuthActions.logout),
                tap(action => {
                    localStorage.removeItem('user');
                    this.router.navigateByUrl('/account/login');
                })
            ),
        { dispatch: false }
    );

    constructor(private actions$: Actions, private router: Router) { }
}