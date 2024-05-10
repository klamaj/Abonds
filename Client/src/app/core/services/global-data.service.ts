import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class GlobalDataService {
    
    public cookies: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    constructor() {}

    // cookies
    dispCookies(value: boolean):void {
        this.cookies.next(value);
    }
}