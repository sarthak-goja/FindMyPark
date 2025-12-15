import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AiService {
  private apiUrl = 'http://localhost:5037/api/AI';

  constructor(private http: HttpClient) { }

  getPrediction(listingId: number, date: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/prediction?listingId=${listingId}&date=${date}`);
  }
}
