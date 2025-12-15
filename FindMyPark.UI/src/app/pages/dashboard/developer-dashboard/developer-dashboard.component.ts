import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeveloperService } from '../../../services/developer.service';

import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-developer-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './developer-dashboard.component.html',
  styleUrls: ['./developer-dashboard.component.css']
})
export class DeveloperDashboardComponent implements OnInit {
  health: any = null;
  flags: any[] = [];
  systemConfig: any = {};
  objectKeys = Object.keys;
  loading = true;

  constructor(private devService: DeveloperService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.loading = true;
    this.devService.getHealth().subscribe(res => this.health = res);
    this.devService.getFlags().subscribe(res => {
      this.flags = Object.keys(res).map(key => ({ key, enabled: res[key] }));
    });
    this.devService.getConfig().subscribe(res => {
      this.systemConfig = res;
      this.loading = false;
    });
  }

  saveConfig() {
    this.devService.updateConfig(this.systemConfig).subscribe(() => {
      alert('Configuration Saved!');
    });
  }

  toggle(key: string, currentVal: boolean) {
    this.devService.toggleFlag(key, !currentVal).subscribe(() => {
      this.loadData();
    });
  }
}
