import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.css'
})
export class NotificationsComponent implements OnInit {
  notifications: any[] = [];
  unreadCount = 0;
  showDropdown = false;

  constructor(private notificationService: NotificationService) { }

  ngOnInit() {
    this.loadNotifications();
  }

  loadNotifications() {
    this.notificationService.getNotifications(1).subscribe({ // Hardcoded User ID 1
      next: (data) => {
        this.notifications = data;
        this.unreadCount = data.filter(n => !n.isRead).length;
      },
      error: (err) => console.error(err)
    });
  }

  toggleDropdown() {
    this.showDropdown = !this.showDropdown;
  }

  markRead(id: number) {
    this.notificationService.markAsRead(id).subscribe(() => {
      this.notifications = this.notifications.map(n => n.id === id ? { ...n, isRead: true } : n);
      this.unreadCount = this.notifications.filter(n => !n.isRead).length;
    });
  }
}
