import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private apiUrl = 'http://localhost:5175/api/Reviews';

  constructor(private http: HttpClient) { }

  getReviews(listingId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/listing/${listingId}`);
  }

  addReview(review: any): Observable<any> {
    return this.http.post(this.apiUrl, review);
  }
}
