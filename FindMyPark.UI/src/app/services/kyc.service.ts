import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KycService {
  private apiUrl = 'http://localhost:5175/api/Kyc';

  constructor(private http: HttpClient) { }

  uploadDocument(userId: number, docType: string, file: any): Observable<any> {
    const formData = new FormData();
    formData.append('userId', userId.toString());
    formData.append('documentType', docType);
    formData.append('file', file);
    return this.http.post(`${this.apiUrl}/upload`, formData);
  }

  getStatus(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/status/${userId}`);
  }

  getPendingUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/pending`);
  }

  verifyUser(userId: number, status: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/verify/${userId}`, JSON.stringify(status), {
      headers: { 'Content-Type': 'application/json' }
    });
  }
}
