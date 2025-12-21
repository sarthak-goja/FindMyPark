import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private apiUrl = 'http://10.0.2.2:5175/api/Vehicles';

  constructor(private http: HttpClient) { }

  getMyVehicles(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}`);
  }

  addVehicle(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  checkChallan(plate: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/check-challan/${plate}`, {});
  }
}
