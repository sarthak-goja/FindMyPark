import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CorporateService } from '../../../services/corporate.service';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-fleet-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './fleet-dashboard.component.html',
  styleUrl: './fleet-dashboard.component.css'
})
export class FleetDashboardComponent implements OnInit {
  dashboardData: any = null;
  loading = true;
  user: any = null;

  constructor(
    private corporateService: CorporateService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.loadDashboard();
  }

  loadDashboard() {
    const userId = 1; // Hardcoded for demo, normally from AuthService
    this.authService.getUser(userId).subscribe({
      next: (u) => {
        this.user = u;
        if (this.user.fleetId) {
          this.corporateService.getDashboard(this.user.fleetId).subscribe({
            next: (data) => {
              this.dashboardData = data;
              this.loading = false;
            },
            error: (err) => {
              console.error(err);
              this.loading = false;
            }
          });
        } else {
          // User not in fleet, maybe show register screen?
          this.loading = false;
        }
      }
    });
  }

  registerFleet() {
    const fleetData = {
      companyName: "My Tech Fleet",
      gstNumber: "GSTIN12345",
      adminUserId: this.user.id
    };
    this.corporateService.registerFleet(fleetData).subscribe(res => {
      alert("Fleet Registered!");
      window.location.reload();
    });
  }
}
