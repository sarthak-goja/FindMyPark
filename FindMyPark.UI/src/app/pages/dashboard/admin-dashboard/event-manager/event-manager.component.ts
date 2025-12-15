import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EventService } from '../../../../services/event.service';

@Component({
  selector: 'app-event-manager',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './event-manager.component.html',
  styleUrls: ['./event-manager.component.css']
})
export class EventManagerComponent implements OnInit {
  activeEvents: any[] = [];
  newEvent = {
    name: '',
    latitude: 28.6139, // Default Delhi
    longitude: 77.2090, // Default Delhi
    radiusMeters: 1000,
    surgeMultiplier: 1.5,
    startTime: '',
    endTime: ''
  };

  constructor(private eventService: EventService) { }

  ngOnInit() {
    this.loadEvents();
  }

  loadEvents() {
    this.eventService.getActiveEvents().subscribe(res => {
      this.activeEvents = res;
    });
  }

  createEvent() {
    const payload = {
      ...this.newEvent,
      startTime: new Date(this.newEvent.startTime),
      endTime: new Date(this.newEvent.endTime),
      isActive: true
    };

    this.eventService.createEvent(payload).subscribe(() => {
      alert('Event Zone Created!');
      this.loadEvents();
      this.resetForm();
    });
  }

  deactivate(id: number) {
    if (confirm('Are you sure you want to stop this surge?')) {
      this.eventService.deactivateEvent(id).subscribe(() => {
        this.loadEvents();
      });
    }
  }

  resetForm() {
    this.newEvent = {
      name: '',
      latitude: 28.6139,
      longitude: 77.2090,
      radiusMeters: 1000,
      surgeMultiplier: 1.5,
      startTime: '',
      endTime: ''
    };
  }
}
