import { createReducer } from "@ngrx/store";
import { User } from "../models/user.model";

export interface AuthState {
    user: User | undefined; 
}

export const initialAuthState: AuthState = {
    user: undefined
}

export const authReducer = createReducer(
    initialAuthState,

    // login
    
)