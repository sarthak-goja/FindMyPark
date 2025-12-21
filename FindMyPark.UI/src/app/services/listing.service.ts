import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ListingService {
  private apiUrl = 'http://localhost:5175/api/Listings';

  constructor(private http: HttpClient) { }

  createListing(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  getMyListings(hostId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/host/${hostId}`);
  }

  getAllListings(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getListingByZone(zoneCode: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/zone/${zoneCode}`);
  }
}
