import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupportService } from '../../../services/support.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-support-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './support-dashboard.component.html',
  styleUrls: ['./support-dashboard.component.css']
})
export class SupportDashboardComponent implements OnInit {
  tickets: any[] = [];
  loading = true;

  constructor(private supportService: SupportService) { }

  ngOnInit() {
    this.loadTickets();
  }

  loadTickets() {
    const userId = 1; // Hardcoded
    this.supportService.getUserTickets(userId).subscribe({
      next: (data) => {
        this.tickets = data;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }
}
