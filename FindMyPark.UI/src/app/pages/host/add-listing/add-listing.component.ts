import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ListingService } from '../../../services/listing.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-listing',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-listing.component.html',
  styleUrl: './add-listing.component.css'
})
export class AddListingComponent {
  listing = {
    title: '',
    address: '',
    latitude: 0,
    longitude: 0,
    pricePerHour: 50,
    isCovered: false,
    hasCCTV: false,
    hasEVCharger: false,
    hostId: 1,
    deviceLatitude: 0,
    deviceLongitude: 0
  };

  isLocating = false;

  constructor(private listingService: ListingService, private router: Router) {
    this.captureDeviceLocation();
  }

  captureDeviceLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.listing.deviceLatitude = position.coords.latitude;
        this.listing.deviceLongitude = position.coords.longitude;
      });
    }
  }

  getCurrentLocation() {
    if (!navigator.geolocation) {
      alert('Geolocation is not supported by your browser');
      return;
    }

    this.isLocating = true;
    navigator.geolocation.getCurrentPosition(
      (position) => {
        this.listing.latitude = position.coords.latitude;
        this.listing.longitude = position.coords.longitude;

        // In a valid "Use Current Location" scenario, the device loc is same as pin loc
        this.listing.deviceLatitude = position.coords.latitude;
        this.listing.deviceLongitude = position.coords.longitude;

        this.listing.address = `GPS Pin: ${position.coords.latitude.toFixed(4)}, ${position.coords.longitude.toFixed(4)}`;
        this.isLocating = false;
      },
      (error) => {
        this.isLocating = false;
        alert('Unable to retrieve your location');
      }
    );
  }

  onSubmit() {
    this.listingService.createListing(this.listing).subscribe({
      next: () => {
        alert('Listing created successfully!');
        this.router.navigate(['/my-listings']);
      },
      error: (err) => {
        console.error(err);
        if (err.error && err.error.message) {
          alert("Error: " + err.error.message);
        } else {
          alert('Failed to create listing');
        }
      }
    });
  }
}
