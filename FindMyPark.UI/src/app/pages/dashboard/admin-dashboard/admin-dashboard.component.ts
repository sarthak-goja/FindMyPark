import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../../services/admin.service';
import { KycService } from '../../../services/kyc.service';
import { EventManagerComponent } from './event-manager/event-manager.component';

import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, EventManagerComponent],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent implements OnInit {
  pendingListings: any[] = [];
  pendingKycUsers: any[] = [];
  isLoading = true;

  heatmapPoints: any[] = [];

  constructor(private adminService: AdminService, private kycService: KycService, private http: HttpClient) { }

  ngOnInit() {
    this.loadPendingListings();
    this.loadPendingKyc();
    this.loadHeatmapData();
  }

  loadHeatmapData() {
    // Mock call to new endpoint
    this.http.get<any[]>('http://localhost:5037/api/Dashboard/analytics/heatmap').subscribe({
      next: (data) => this.heatmapPoints = data,
      error: (err) => console.error('Failed to load heatmap', err)
    });
  }

  loadPendingListings() {
    this.adminService.getPendingListings().subscribe({
      next: (data) => {
        this.pendingListings = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
      }
    });
  }

  approve(id: number) {
    if (confirm('Are you sure you want to approve this listing?')) {
      this.adminService.approveListing(id).subscribe({
        next: () => {
          alert('Listing Approved!');
          this.loadPendingListings();
        },
        error: (err) => alert('Failed to approve')
      });
    }
  }

  loadPendingKyc() {
    this.kycService.getPendingUsers().subscribe({
      next: (data) => this.pendingKycUsers = data,
      error: (err) => console.error(err)
    });
  }

  verifyKyc(userId: number, status: string) {
    if (confirm(`Mark user as ${status}?`)) {
      this.kycService.verifyUser(userId, status).subscribe({
        next: () => {
          alert(`User ${status}!`);
          this.loadPendingKyc();
        },
        error: (err) => alert('Action Failed')
      });
    }
  }
}
