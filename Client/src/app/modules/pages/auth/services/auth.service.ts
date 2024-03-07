import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { User } from "../models/user.model";

@Injectable()
export class AuthService {

    constructor (private http: HttpClient) {}

    // login
    login(obj: Object): Observable<User> {
        return this.http.post<User>(`${environment.apiUrl}/Auth/login`, obj);
    }

    // Forgot Password
    forgotPassword(obj: any) {
        return this.http.post(`${environment.apiUrl}/Auth/forgot-password`, obj);
    }

    // Reset Password
    resetPassword(obj: any) {
        return this.http.post(`${environment.apiUrl}/Auth/reset-password`, obj);
    }
}