import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdService {
  private apiUrl = 'http://localhost:5175/api/Ad';

  constructor(private http: HttpClient) { }

  getAds(city: string = 'All'): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}?city=${city}`);
  }
}
