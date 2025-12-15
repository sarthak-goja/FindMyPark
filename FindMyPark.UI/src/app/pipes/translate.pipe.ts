import { Pipe, PipeTransform } from '@angular/core';
import { LanguageService } from '../services/language.service';

@Pipe({
  name: 'translate',
  standalone: true,
  pure: false // Impure to detect signal changes? Or better to use signal in template
})
export class TranslatePipe implements PipeTransform {
  constructor(private langService: LanguageService) { }

  transform(value: string): string {
    return this.langService.translate(value);
  }
}
