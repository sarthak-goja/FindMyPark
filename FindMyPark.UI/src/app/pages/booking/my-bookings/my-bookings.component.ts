import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../../services/booking.service';
import { ReviewModalComponent } from '../../../components/reviews/review-modal/review-modal.component';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-my-bookings',
  standalone: true,
  imports: [CommonModule, ReviewModalComponent],
  templateUrl: './my-bookings.component.html',
  styleUrl: './my-bookings.component.css'
})
export class MyBookingsComponent implements OnInit {
  bookings: any[] = [];
  isLoading = true;
  showReviewModal = false;
  selectedBookingId: number | null = null;

  constructor(
    private bookingService: BookingService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.authService.currentUser$.subscribe(user => {
      if (user) {
        this.bookingService.getMyBookings(user.id).subscribe({
          next: (data) => {
            console.log('MyBookings Loaded:', data);
            this.bookings = data;
            this.isLoading = false;
          },
          error: (err) => {
            console.error('MyBookings Error:', err);
            this.isLoading = false;
          }
        });
      }
    });
  }

  openReview(bookingId: number) {
    this.selectedBookingId = bookingId;
    this.showReviewModal = true;
  }

  cancelBooking(bookingId: number) {
    if (confirm('Are you sure you want to cancel this booking? Refund will be credited to your wallet.')) {
      this.bookingService.cancelBooking(bookingId).subscribe({
        next: () => {
          alert('Booking cancelled successfully.');
          this.ngOnInit(); // Reload
        },
        error: (err) => alert('Failed to cancel booking')
      });
    }
  }

  extendBooking(booking: any) {
    const currentEnd = new Date(booking.endTime);
    const suggestedEnd = new Date(currentEnd.getTime() + 60 * 60 * 1000); // +1 Hour default
    // Format to YYYY-MM-DDTHH:mm for prompt helper or just ask user
    // For MVP, using prompt is tricky for Date. Let's use a simple approach:
    // Ask for number of hours to extend ? Or just default 1 hour?
    // Let's ask via Prompt: "Enter new End Time (YYYY-MM-DDTHH:mm)" or simpler: "Extend by how many hours?"

    // Better: prompt for hours.
    const hoursStr = prompt('Enter hours to extend (e.g. 1 or 2):', '1');
    if (hoursStr) {
      const hours = parseFloat(hoursStr);
      if (isNaN(hours) || hours <= 0) return alert('Invalid hours');

      const newEnd = new Date(currentEnd.getTime() + hours * 60 * 60 * 1000);

      this.bookingService.extendBooking(booking.id, newEnd.toISOString()).subscribe({
        next: (res) => {
          alert(`Extended! Cost: ₹${res.additionalCost}`);
          this.ngOnInit();
        },
        error: (err) => alert(err.error || 'Failed to extend')
      });
    }
  }
}
