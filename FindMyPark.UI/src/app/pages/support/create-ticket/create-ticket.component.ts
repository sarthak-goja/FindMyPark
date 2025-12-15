import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SupportService } from '../../../services/support.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-ticket',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-ticket.component.html',
  styleUrls: ['./create-ticket.component.css']
})
export class CreateTicketComponent {
  ticket = {
    subject: '',
    description: ''
  };
  isSubmitting = false;

  constructor(private supportService: SupportService, private router: Router) { }

  onSubmit() {
    this.isSubmitting = true;
    const data = {
      userId: 1, // Hardcoded for demo
      ...this.ticket
    };

    this.supportService.createTicket(data).subscribe({
      next: () => {
        alert('Ticket Created!');
        this.router.navigate(['/support/dashboard']);
      },
      error: (err) => {
        console.error(err);
        this.isSubmitting = false;
      }
    });
  }
}
