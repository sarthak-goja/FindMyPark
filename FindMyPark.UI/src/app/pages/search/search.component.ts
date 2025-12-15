import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListingService } from '../../services/listing.service';
import mapboxgl from 'mapbox-gl';
import { AdBannerComponent } from '../../components/shared/ad-banner/ad-banner.component';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, AdBannerComponent],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements AfterViewInit {
  private map!: mapboxgl.Map;
  listings: any[] = [];

  // Public Demo Token
  private mapboxToken = 'pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4M29iazA2Z2gycXA4N2pmbDZmangifQ.-g_vE53SD2WrJ6tFX7QHmA';

  constructor(private listingService: ListingService) { }

  ngAfterViewInit(): void {
    this.initMap();
    this.fetchListings();
  }

  private initMap(): void {
    (mapboxgl as any).accessToken = this.mapboxToken;

    this.map = new mapboxgl.Map({
      container: 'map', // container ID
      style: 'mapbox://styles/mapbox/streets-v12', // style URL
      center: [77.2090, 28.6139], // starting position [lng, lat]
      zoom: 12 // starting zoom
    });

    this.map.addControl(new mapboxgl.NavigationControl());

    this.map.on('load', () => {
      console.log('Mapbox loaded successfully');
    });

    this.map.on('error', (e) => {
      console.error('Mapbox error:', e);
    });
  }

  private fetchListings(): void {
    this.listingService.getAllListings().subscribe({
      next: (data) => {
        this.listings = data;
        this.addMarkers();
      },
      error: (err) => console.error('Error fetching listings:', err)
    });
  }

  private addMarkers(): void {
    this.listings.forEach(listing => {
      // Create a DOM element for each marker.
      const el = document.createElement('div');
      el.className = 'marker';
      el.style.backgroundColor = listing.isOccupied ? '#E74C3C' : '#2ECC71';
      el.style.width = '24px';
      el.style.height = '24px';
      el.style.borderRadius = '50%';
      el.style.cursor = 'pointer';
      el.style.border = '2px solid white';
      el.style.boxShadow = '0 2px 4px rgba(0,0,0,0.3)';

      // Add popup
      const popup = new mapboxgl.Popup({ offset: 25 }).setHTML(`
        <div style="color:black; min-width:150px; text-align:center;">
            <b>${listing.title}</b><br>
            ${listing.isOccupied ? '🔴 Busy' : '🟢 Available'}<br>
            <div style="margin:5px 0;">₹${listing.pricePerHour}/hr</div>
             <a href="/book?listingId=${listing.id}&title=${encodeURIComponent(listing.title)}&price=${listing.pricePerHour}" 
                style="display:inline-block; padding:6px 12px; background:#3498DB; color:white; text-decoration:none; border-radius:4px; font-weight:bold;">
                Book Now
             </a>
        </div>
      `);

      new mapboxgl.Marker(el)
        .setLngLat([listing.longitude, listing.latitude])
        .setPopup(popup)
        .addTo(this.map);
    });
  }
}
