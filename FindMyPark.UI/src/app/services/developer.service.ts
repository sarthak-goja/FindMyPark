import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeveloperService {
  private apiUrl = 'http://localhost:5037/api/Developer';

  constructor(private http: HttpClient) { }

  getHealth(): Observable<any> {
    return this.http.get(`${this.apiUrl}/health`);
  }

  getFlags(): Observable<any> {
    return this.http.get(`${this.apiUrl}/flags`);
  }

  toggleFlag(key: string, enabled: boolean): Observable<any> {
    return this.http.post(`${this.apiUrl}/flags/${key}?enabled=${enabled}`, {});
  }

  getConfig(): Observable<any> {
    return this.http.get(`${this.apiUrl}/config`);
  }

  updateConfig(config: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/config`, config);
  }
}
