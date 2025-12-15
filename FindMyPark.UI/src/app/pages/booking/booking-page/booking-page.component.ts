import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingService } from '../../../services/booking.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AiService } from '../../../services/ai.service';
import { ServicesService } from '../../../services/services.service';
import { EvService } from '../../../services/ev.service';

@Component({
  selector: 'app-booking-page',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './booking-page.component.html',
  styleUrl: './booking-page.component.css'
})
export class BookingPageComponent implements OnInit {
  listingId = 0;
  listingTitle = '';
  pricePerHour = 0;

  startTime: string = '';
  endTime: string = '';
  totalPrice = 0;
  isProcessing = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookingService: BookingService,
    private aiService: AiService,
    private servicesService: ServicesService,
    private evService: EvService
  ) { }

  forecast: any[] = [];
  services: any[] = [];
  selectedServices: any[] = [];
  chargers: any[] = [];

  loadPrediction() {
    const date = this.startTime ? this.startTime.split('T')[0] : new Date().toISOString().split('T')[0];
    this.aiService.getPrediction(this.listingId, date).subscribe(data => {
      this.forecast = data;
    });
  }

  loadChargers() {
    this.evService.getChargers(this.listingId).subscribe(data => {
      this.chargers = data;
    });
  }

  loadServices() {
    this.servicesService.getServices().subscribe(data => {
      this.services = data;
    });
  }

  toggleService(service: any) {
    const idx = this.selectedServices.findIndex(s => s.id === service.id);
    if (idx > -1) {
      this.selectedServices.splice(idx, 1);
    } else {
      this.selectedServices.push(service);
    }
  }

  get servicesTotal(): number {
    return this.selectedServices.reduce((sum, s) => sum + s.price, 0);
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.listingId = params['listingId'];
      this.listingTitle = params['title'];
      this.pricePerHour = +params['price'];
      this.loadPrediction();
      this.loadServices();
      this.loadChargers();
    });
  }

  isSurge = false;
  promoCode = '';
  appliedPromo: any = null;
  discount = 0;

  applyPromo() {
    if (this.promoCode === 'SAVE50') {
      this.appliedPromo = { code: 'SAVE50', type: 'percentage', value: 50 };
      this.discount = (this.totalPrice * 0.5);
      alert('Promo Applied: 50% Off!');
    } else {
      alert('Invalid Code');
      this.appliedPromo = null;
      this.discount = 0;
    }
  }

  calculatePrice() {
    if (this.startTime && this.endTime) {
      // Call Backend for Accurate Dynamic Price
      this.bookingService.getEstimatedPrice(this.listingId, this.startTime, this.endTime).subscribe({
        next: (res: any) => {
          this.totalPrice = res.totalPrice;
          // specific logic to check if price > base calc
          const start = new Date(this.startTime);
          const end = new Date(this.endTime);
          const hours = (end.getTime() - start.getTime()) / 3600000;
          this.isSurge = res.totalPrice > (res.baseRate * hours);
        },
        error: (err) => {
          console.error(err);
          // Fallback
          const start = new Date(this.startTime);
          const end = new Date(this.endTime);
          const diffHrs = (end.getTime() - start.getTime()) / (1000 * 60 * 60);
          this.totalPrice = diffHrs > 0 ? diffHrs * this.pricePerHour : 0;
        }
      });
    }
  }
  confirmBooking() {
    this.isProcessing = true;
    const bookingData = {
      listingId: this.listingId,
      driverId: 3, // Hardcoded driver (ID 3 from Seed)
      startTime: this.startTime,
      endTime: this.endTime
    };

    this.bookingService.createBooking(bookingData).subscribe({
      next: (response) => {
        alert('Booking Confirmed!');
        this.router.navigate(['/bookings']);
      },
      error: (err) => {
        this.isProcessing = false;
        console.error(err);
        if (err.status === 400 && err.error?.includes('Insufficient funds')) {
          if (confirm('Insufficient funds in wallet. Go to Wallet to add money?')) {
            this.router.navigate(['/wallet']);
          }
        } else {
          alert('Booking Failed: ' + (err.error || 'Unknown Error'));
        }
      }
    });
  }
}

