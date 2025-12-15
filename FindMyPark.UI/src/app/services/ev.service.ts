import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EvService {
  private apiUrl = 'http://localhost:5175/api/EV/chargers';

  constructor(private http: HttpClient) { }

  getChargers(listingId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${listingId}`);
  }
}
