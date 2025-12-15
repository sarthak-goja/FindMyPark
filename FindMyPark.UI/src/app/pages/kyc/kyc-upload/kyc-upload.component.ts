import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KycService } from '../../../services/kyc.service';

@Component({
  selector: 'app-kyc-upload',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './kyc-upload.component.html',
  styleUrl: './kyc-upload.component.css'
})
export class KycUploadComponent implements OnInit {
  status = 'None';
  documents = [
    { type: 'Aadhaar', uploaded: false, file: null },
    { type: 'PAN', uploaded: false, file: null },
    { type: 'Selfie', uploaded: false, file: null }
  ];

  constructor(private kycService: KycService) { }

  ngOnInit() {
    this.checkStatus();
  }

  checkStatus() {
    this.kycService.getStatus(1).subscribe({ // Hardcoded User ID 1
      next: (res) => this.status = res.status,
      error: (err) => console.error(err)
    });
  }

  onFileSelected(event: any, docType: string) {
    const file = event.target.files[0];
    if (file) {
      this.upload(docType, file);
    }
  }

  upload(docType: string, file: any) {
    this.kycService.uploadDocument(1, docType, file).subscribe({
      next: () => {
        const doc = this.documents.find(d => d.type === docType);
        if (doc) doc.uploaded = true;
        this.checkStatus();
      },
      error: (err) => alert('Upload Failed')
    });
  }
}
