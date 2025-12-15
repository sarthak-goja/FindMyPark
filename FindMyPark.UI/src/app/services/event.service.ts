import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private apiUrl = 'http://localhost:5175/api/Events';

  constructor(private http: HttpClient) { }

  createEvent(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  getActiveEvents(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/active`);
  }

  deactivateEvent(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/deactivate`, {});
  }
}
