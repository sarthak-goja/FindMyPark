import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SupportService {
  private apiUrl = 'http://10.0.2.2:5175/api/Support';

  constructor(private http: HttpClient) { }

  createTicket(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  getUserTickets(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}`);
  }

  getAllTickets(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/all`);
  }

  resolveTicket(id: number, response: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/resolve`, { response });
  }
}
