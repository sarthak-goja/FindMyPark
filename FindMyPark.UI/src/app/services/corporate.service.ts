import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CorporateService {
  private apiUrl = 'http://localhost:5037/api/Corporate';

  constructor(private http: HttpClient) { }

  registerFleet(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  getDashboard(fleetId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/dashboard/${fleetId}`);
  }
}
