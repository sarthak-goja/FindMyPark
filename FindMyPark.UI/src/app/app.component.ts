import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { ThemeService } from './services/theme.service';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { ChatWidgetComponent } from './components/shared/chat-widget/chat-widget.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavbarComponent, ChatWidgetComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'find-my-park.ui';

  constructor(private themeService: ThemeService) { }

  ngOnInit() {
    this.themeService.loadTheme();
  }
}
