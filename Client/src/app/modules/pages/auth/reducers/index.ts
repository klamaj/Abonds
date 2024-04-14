import { createReducer, on } from "@ngrx/store";
import { User } from "../models/user.model";
import { AuthActions } from "../services/action-types";

export interface AuthState {
    user: User | undefined; 
}

export const initialAuthState: AuthState = {
    user: undefined
}

export const authReducer = createReducer(
    initialAuthState,

    // login
    on(AuthActions.login, (state, action) => {
        return {
            user: action.user
        }
    }),

    on(AuthActions.logout, (state, action) => {
        return {
            user: undefined
        }
    })
)