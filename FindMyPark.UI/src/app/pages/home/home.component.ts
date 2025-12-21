import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ListingService } from '../../services/listing.service';
import { AuthService } from '../../services/auth.service';
import { BookingService } from '../../services/booking.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy {
  zoneCode: string = '';
  errorMessage: string = '';

  constructor(
    private listingService: ListingService,
    private router: Router,
    private authService: AuthService,
    private bookingService: BookingService
  ) { }

  startParking() {
    if (!this.zoneCode) return;

    this.listingService.getListingByZone(this.zoneCode).subscribe({
      next: (listing) => {
        this.router.navigate(['/book'], {
          queryParams: {
            listingId: listing.id,
            title: listing.title,
            price: listing.pricePerHour
          }
        });
      },
      error: () => {
        this.errorMessage = 'Invalid Zone Code. Please try again.';
      }
    });
  }

  // Active Session Mock (for demo)
  activeSession: boolean = false;
  timerString: string = "00:00:00";

  ngOnInit() {
    this.authService.currentUser$.subscribe(user => {
      if (user) {
        this.bookingService.getLastParked(user.id).subscribe({
          next: (res: any) => {
            if (res.isActive) {
              this.activeSession = true;
              const endTime = new Date(res.endTime).getTime();
              const now = Date.now();
              const diffSeconds = Math.floor((endTime - now) / 1000);

              if (diffSeconds > 0) {
                this.startTimer(diffSeconds);
              }
            }
          },
          error: (err) => console.log('No active session found')
        });
      }
    });
  }

  intervalId: any;

  startTimer(duration: number) {
    if (this.intervalId) clearInterval(this.intervalId);

    this.intervalId = setInterval(() => {
      duration--;
      if (duration <= 0) {
        this.activeSession = false;
        clearInterval(this.intervalId);
        return;
      }
      const hrs = Math.floor(duration / 3600);
      const mins = Math.floor((duration % 3600) / 60);
      const secs = duration % 60;
      this.timerString = `${hrs.toString().padStart(2, '0')}:${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
    }, 1000);
  }
  ngOnDestroy() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }
}
