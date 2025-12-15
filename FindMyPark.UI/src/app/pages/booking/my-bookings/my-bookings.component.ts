import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../../services/booking.service';
import { ReviewModalComponent } from '../../../components/reviews/review-modal/review-modal.component';

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

  constructor(private bookingService: BookingService) { }

  ngOnInit() {
    this.bookingService.getMyBookings(1).subscribe({ // Hardcoded User ID 1
      next: (data) => {
        this.bookings = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
      }
    });
  }

  openReview(bookingId: number) {
    this.selectedBookingId = bookingId;
    this.showReviewModal = true;
  }
}
