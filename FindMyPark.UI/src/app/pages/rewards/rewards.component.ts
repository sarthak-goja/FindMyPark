import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-rewards',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './rewards.component.html',
  styleUrl: './rewards.component.css'
})
export class RewardsComponent implements OnInit {
  points = 0;
  level = 'Bronze';
  nextLevelPoints = 100;
  progress = 0;
  badges: string[] = [];

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.getUser(1).subscribe({ // Hardcoded User ID 1
      next: (user: any) => {
        this.points = user.points || 0;
        this.calculateLevel();
      }
    });
  }

  calculateLevel() {
    if (this.points >= 500) {
      this.level = 'Gold';
      this.badges = ['Early Adopter', 'Super Parker', 'Gold Member'];
      this.progress = 100;
    } else if (this.points >= 100) {
      this.level = 'Silver';
      this.nextLevelPoints = 500;
      this.badges = ['Early Adopter', 'Silver Member'];
      this.progress = ((this.points - 100) / 400) * 100;
    } else {
      this.level = 'Bronze';
      this.nextLevelPoints = 100;
      this.badges = ['Early Adopter'];
      this.progress = (this.points / 100) * 100;
    }
  }
}
