import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contract } from '../models/contract.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ClientAnswers, ClientQuestionCat } from '../models/client-answers.model';
import { Single } from '../models/single.model';
import { Client } from '../models/client.model';
import { ClientInterest } from '../models/client-interests.model';

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
  setClientStatus(status: any): string {
    switch (Number(status)) {
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

  // Get Singles
  getSingles(type: string): Observable<Single[]> {
    return this.http.get<Single[]>(`${environment.apiUrl}/Clients/Singles?gender=${type}`);
  }

  // Get Client By Id
  getClientById(id: number): Observable<Client> {
    return this.http.get<Client>(`${environment.apiUrl}/Clients/${id}`);
  }

  // UploadDocument
  uploadContractToClient(file: any, id: number): Observable<Contract> {
    const formData = new FormData();
    formData.append("file", file);
    return this.http.post<Contract>(`${environment.apiUrl}/Clients/${id}/Contract`, formData);
  }

  // Send Questions
  sendQuestions(clientId: number, questionId: number) {
    return this.http.post(`${environment.apiUrl}/Clients/${clientId}/SendQuestion/${questionId}`, null);
  }

  // Get Ineterests
  getInterest(clientId: number): Observable<ClientInterest[]> {
    return this.http.get<ClientInterest[]>(`${environment.apiUrl}/Clients/${clientId}/Interests`);
  }
}
