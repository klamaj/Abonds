import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contract } from '../models/contract.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ClientAnswers, ClientQuestionCat } from '../models/client-answers.model';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private http: HttpClient) { }

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

  // Send Message to Client
  sendMessage(clientId: number, obj: any): Observable<string> {
    return this.http.post<string>(`${environment.apiUrl}/Clients/${clientId}/SendMessage`, obj);
  }

  // Get Client Answers
  getAnswers(clientId: any): Observable<ClientQuestionCat[]> {
    return this.http.get<ClientQuestionCat[]>(`${environment.apiUrl}/Clients/${clientId}/Answers`);
  }
}
