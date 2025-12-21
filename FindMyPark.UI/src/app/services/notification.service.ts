import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private apiUrl = 'http://10.0.2.2:5175/api/Notifications';

  constructor(private http: HttpClient) { }

  getNotifications(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${userId}`);
  }

  markAsRead(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/mark-read/${id}`, {});
  }
}
