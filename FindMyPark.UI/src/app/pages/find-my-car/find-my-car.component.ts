import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../services/booking.service';
import mapboxgl from 'mapbox-gl';

@Component({
  selector: 'app-find-my-car',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './find-my-car.component.html',
  styleUrl: './find-my-car.component.css'
})
export class FindMyCarComponent implements OnInit {
  parkedDetails: any = null;
  isLoading = true;
  map!: mapboxgl.Map;
  private mapboxToken = 'pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4M29iazA2Z2gycXA4N2pmbDZmangifQ.-g_vE53SD2WrJ6tFX7QHmA';

  constructor(private bookingService: BookingService) { }

  ngOnInit() {
    this.bookingService.getLastParked(1).subscribe({ // Hardcoded User ID 1
      next: (data) => {
        this.parkedDetails = data;
        this.isLoading = false;
        setTimeout(() => this.initMap(), 100); // Wait for DOM
      },
      error: (err) => {
        this.isLoading = false;
      }
    });
  }

  initMap() {
    if (!this.parkedDetails) return;

    (mapboxgl as any).accessToken = this.mapboxToken;

    this.map = new mapboxgl.Map({
      container: 'map',
      style: 'mapbox://styles/mapbox/streets-v12',
      center: [this.parkedDetails.longitude, this.parkedDetails.latitude],
      zoom: 15
    });

    // Create a DOM element for the marker
    const el = document.createElement('div');
    el.className = 'car-marker';
    el.style.backgroundImage = 'url(https://cdn-icons-png.flaticon.com/512/3202/3202926.png)'; // Car icon
    el.style.width = '40px';
    el.style.height = '40px';
    el.style.backgroundSize = 'contain';

    const popup = new mapboxgl.Popup({ offset: 25 }).setHTML(`<b>${this.parkedDetails.listingTitle}</b><br>Parked Here`);

    new mapboxgl.Marker(el)
      .setLngLat([this.parkedDetails.longitude, this.parkedDetails.latitude])
      .setPopup(popup)
      .addTo(this.map);
  }

  navigate() {
    if (!this.parkedDetails) return;
    const url = `https://www.google.com/maps/dir/?api=1&destination=${this.parkedDetails.latitude},${this.parkedDetails.longitude}`;
    window.open(url, '_blank');
  }
}
