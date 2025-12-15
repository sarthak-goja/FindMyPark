import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-chat-widget',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat-widget.component.html',
  styleUrls: ['./chat-widget.component.css']
})
export class ChatWidgetComponent implements OnInit {
  isOpen = false;
  faqs: any[] = [];
  searchQuery = '';

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<any[]>('http://localhost:5037/api/Support/faqs').subscribe(data => {
      this.faqs = data;
    });
  }

  toggleChat() {
    this.isOpen = !this.isOpen;
  }

  get filteredFaqs() {
    if (!this.searchQuery) return this.faqs;
    return this.faqs.filter(f => f.question.toLowerCase().includes(this.searchQuery.toLowerCase()));
  }

  selectFaq(faq: any) {
    alert(`Answer: ${faq.answer}`);
  }
}
