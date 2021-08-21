import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { fieldLocalStorage } from '../constants/local-storage.constants';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private isDarkTheme = false;
  private darkThemeString = 'dark-theme';
  private subjectIsDarkTheme: BehaviorSubject<boolean>;

  constructor() {
    const themeString = localStorage.getItem(fieldLocalStorage.theme);
    if (themeString) {
      this.isDarkTheme = themeString === this.darkThemeString;
      document.body.classList.add(this.darkThemeString);
    }
    this.subjectIsDarkTheme = new BehaviorSubject<boolean>(
      this.isDarkTheme
    );
  }

  public toggleTheme(): void {
    this.isDarkTheme = !this.isDarkTheme;
    this.subjectIsDarkTheme.next(this.isDarkTheme);
    if (this.isDarkTheme) {
      document.body.classList.add(this.darkThemeString);
      localStorage.setItem(fieldLocalStorage.theme, this.darkThemeString);
    } else {
      document.body.classList.remove(this.darkThemeString);
      localStorage.removeItem(fieldLocalStorage.theme);
    }
  }

  public hasDarkTheme(): boolean {
    return this.isDarkTheme;
  }

  public getChangingThemeSubject(): BehaviorSubject<boolean> {
    return this.subjectIsDarkTheme;
  }
}
