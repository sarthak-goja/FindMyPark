import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  currentLang = signal<'en' | 'hi'>('en');

  private translations: any = {
    'en': {
      'NAV_HOME': 'Home',
      'NAV_BOOKINGS': 'My Bookings',
      'NAV_WALLET': 'Wallet',
      'NAV_LOGIN': 'Login',
      'HERO_TITLE': 'Find Parking in Seconds',
      'HERO_SUB': 'Smart, Secure, and Seamless parking across Delhi NCR.',
      'BTN_FindSpot': 'Find a Spot'
    },
    'hi': {
      'NAV_HOME': 'होम',
      'NAV_BOOKINGS': 'मेरी बुकिंग',
      'NAV_WALLET': 'वॉलेट',
      'NAV_LOGIN': 'लॉग इन',
      'HERO_TITLE': 'सेकंड में पार्किंग खोजें',
      'HERO_SUB': 'दिल्ली एनसीआर में स्मार्ट, सुरक्षित और निर्बाध पार्किंग।',
      'BTN_FindSpot': 'जगह खोजें'
    }
  };

  setLanguage(lang: 'en' | 'hi') {
    this.currentLang.set(lang);
  }

  translate(key: string): string {
    const lang = this.currentLang();
    return this.translations[lang][key] || key;
  }

  toggleLanguage() {
    this.setLanguage(this.currentLang() === 'en' ? 'hi' : 'en');
  }
}
