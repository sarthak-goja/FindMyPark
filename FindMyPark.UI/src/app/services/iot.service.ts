import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IotService {
  private apiUrl = 'http://localhost:5175/api/IoT';

  constructor(private http: HttpClient) { }

  simulateToggle(listingId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/simulate/${listingId}`, {});
  }
}
