import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";

@Injectable()
export class AuthService {

    constructor (private http: HttpClient) {}

    // Forgot Password
    forgotPassword(obj: any) {
        return this.http.post(`${environment.apiUrl}/Auth/forgot-password`, obj);
    }

    // Reset Password
    resetPassword(obj: any) {
        return this.http.post(`${environment.apiUrl}/Auth/reset-password`, obj);
    }
}