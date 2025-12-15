import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VehicleService } from '../../../services/vehicle.service';

@Component({
  selector: 'app-my-vehicles',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './my-vehicles.component.html',
  styleUrl: './my-vehicles.component.css'
})
export class MyVehiclesComponent implements OnInit {
  vehicles: any[] = [];
  newVehicle = {
    licensePlate: '',
    type: '4W',
    model: ''
  };
  showForm = false;
  checkingChallan = false;
  challanResult: any = null;

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.loadVehicles();
  }

  loadVehicles() {
    this.vehicleService.getMyVehicles(1).subscribe({ // Hardcoded User ID 1
      next: (data) => this.vehicles = data,
      error: (err) => console.error(err)
    });
  }

  addVehicle() {
    const data = { ...this.newVehicle, userId: 1 };
    this.vehicleService.addVehicle(data).subscribe({
      next: (res) => {
        this.vehicles.push(res);
        this.newVehicle = { licensePlate: '', type: '4W', model: '' };
        this.showForm = false;
        alert('Vehicle added successfully!');
      },
      error: (err) => console.error(err)
    });
  }

  checkChallan(plate: string) {
    this.checkingChallan = true;
    this.challanResult = null;
    this.vehicleService.checkChallan(plate).subscribe({
      next: (res) => {
        this.challanResult = res;
        this.checkingChallan = false;
      },
      error: (err) => this.checkingChallan = false
    });
  }
}
