import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AdminService {
    private apiUrl = 'http://10.0.2.2:5175/api/Listings';

    constructor(private http: HttpClient) { }

    getPendingListings(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}/pending`);
    }

    approveListing(id: number): Observable<any> {
        return this.http.post(`${this.apiUrl}/${id}/approve`, {});
    }
}
