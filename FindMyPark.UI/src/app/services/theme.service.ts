import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private apiUrl = 'http://localhost:5175/api/Tenant/config';

  constructor(private http: HttpClient) { }

  loadTheme() {
    // Simulation: Check query param 'tenant' or default
    // e.g. ?tenant=dlf
    const urlParams = new URLSearchParams(window.location.search);
    const domain = urlParams.get('tenant') || 'default';

    this.http.get<any>(`${this.apiUrl}?domain=${domain}`).subscribe({
      next: (config) => {
        if (config) {
          this.applyTheme(config);
        }
      },
      error: (err) => console.error('Failed to load theme', err)
    });
  }

  private applyTheme(config: any) {
    const root = document.documentElement;
    if (config.primaryColor) {
      root.style.setProperty('--color-primary', config.primaryColor);
      // Calculate a darker variant for hover/secondary if needed
      // For now, just setting primary
    }
    if (config.name) {
      document.title = config.name;
    }
    // Store logo for components to usage
    if (config.logoUrl) {
      localStorage.setItem('tenantLogo', config.logoUrl);
    }
  }
}
