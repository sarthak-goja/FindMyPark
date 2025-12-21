import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WalletService } from '../../services/wallet.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-wallet',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.css'
})
export class WalletComponent implements OnInit {
  wallet: any = null;
  isLoading = true;
  amountToAdd: number = 500;

  constructor(
    private walletService: WalletService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.authService.currentUser$.subscribe(user => {
      if (user) {
        this.loadWallet(user.id);
      }
    });
  }

  loadWallet(userId: number) {
    this.walletService.getWallet(userId).subscribe({
      next: (data) => {
        this.wallet = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
      }
    });
  }

  addMoney() {
    this.authService.currentUser$.subscribe(user => {
      if (user) {
        this.walletService.addFunds(user.id, this.amountToAdd).subscribe({
          next: () => {
            alert('Funds added successfully!');
            this.loadWallet(user.id);
          },
          error: (err) => alert('Failed to add funds')
        });
      }
    });
  }
}
