import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WalletService } from '../../services/wallet.service';

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

  constructor(private walletService: WalletService) { }

  ngOnInit() {
    this.loadWallet();
  }

  loadWallet() {
    this.walletService.getWallet(1).subscribe({ // Hardcoded User ID 1
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
    this.walletService.addFunds(1, this.amountToAdd).subscribe({
      next: () => {
        alert('Funds added successfully!');
        this.loadWallet();
      },
      error: (err) => alert('Failed to add funds')
    });
  }
}
