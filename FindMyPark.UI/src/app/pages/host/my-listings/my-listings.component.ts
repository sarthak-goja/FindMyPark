import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ListingService } from '../../../services/listing.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-my-listings',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './my-listings.component.html',
  styleUrl: './my-listings.component.css'
})
export class MyListingsComponent implements OnInit {
  listings: any[] = [];
  isLoading = true;

  constructor(private listingService: ListingService) { }

  ngOnInit() {
    // Hardcoded Host ID 1 for now
    this.listingService.getMyListings(1).subscribe({
      next: (data) => {
        this.listings = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
      }
    });
  }
}
