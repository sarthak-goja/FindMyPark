import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WalletService {
  private apiUrl = 'http://10.0.2.2:5175/api/Wallet';

  constructor(private http: HttpClient) { }

  getWallet(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${userId}`);
  }

  addFunds(userId: number, amount: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/add-funds`, { userId, amount });
  }
}
