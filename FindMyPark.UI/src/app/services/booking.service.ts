import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = 'http://localhost:5175/api/Bookings';

  constructor(private http: HttpClient) { }

  createBooking(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  getMyBookings(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}`);
  }

  getLastParked(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/last-parked/${userId}`);
  }

  validatePromo(code: string): Observable<any> {
    return this.http.get<any>(`http://localhost:5175/api/Promotions/validate/${code}`);
  }

  getEstimatedPrice(listingId: number, start: string, end: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/calculate-price?listingId=${listingId}&start=${start}&end=${end}`);
  }
}
