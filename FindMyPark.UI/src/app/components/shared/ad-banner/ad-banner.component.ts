import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdService } from '../../../services/ad.service';

@Component({
  selector: 'app-ad-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ad-banner.component.html',
  styleUrls: ['./ad-banner.component.css']
})
export class AdBannerComponent implements OnInit {
  @Input() city: string = 'All';
  ads: any[] = [];
  currentAd: any = null;

  constructor(private adService: AdService) { }

  ngOnInit() {
    this.adService.getAds(this.city).subscribe(ads => {
      this.ads = ads;
      if (this.ads.length > 0) {
        this.currentAd = this.ads[0];
        // Rotate ads every 10 seconds
        setInterval(() => {
          const idx = Math.floor(Math.random() * this.ads.length);
          this.currentAd = this.ads[idx];
        }, 10000);
      }
    });
  }
}
