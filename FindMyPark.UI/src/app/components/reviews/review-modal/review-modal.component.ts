import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReviewService } from '../../../services/review.service';

@Component({
  selector: 'app-review-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './review-modal.component.html',
  styleUrl: './review-modal.component.css'
})
export class ReviewModalComponent {
  @Input() bookingId!: number;
  @Output() close = new EventEmitter<void>();

  rating = 5;
  comment = '';
  isSubmitting = false;

  constructor(private reviewService: ReviewService) { }

  submitReview() {
    this.isSubmitting = true;
    const review = {
      bookingId: this.bookingId,
      driverId: 1, // Hardcoded driver
      rating: this.rating,
      comment: this.comment
    };

    this.reviewService.addReview(review).subscribe({
      next: () => {
        alert('Review Submitted!');
        this.close.emit();
      },
      error: (err) => {
        console.error(err);
        this.isSubmitting = false;
        alert('Failed to submit review');
      }
    });
  }
}
