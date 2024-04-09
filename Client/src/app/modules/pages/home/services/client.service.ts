import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor() { }

  // Calculate Age
  calculateAge(value: string | any): number {
    let newDate = new Date(value!);
    let timeDiff = Math.abs(Date.now() - newDate.getTime());
    let age = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);
    return age;
  }

  // Set Client Status
  setClientStatus(status: number): string {
    switch (status) {
      case 0: return "Single";
      case 1: return "Divorced";
      case 2: return "InRelationship";
      default: return "Undefined";
    }
  }
}
