import { Injectable } from '@angular/core';
import { ThemeService } from './theme.service';

@Injectable({ providedIn: 'root' })
export class ColorPaletteService {
  public blackPalette = [
    { color: '#424242', title: 'Default' },
    { color: '#5c2b29', title: 'Red' },
    { color: '#614a19', title: 'Orange' },
    { color: '#635d19', title: 'Yellow' },
    { color: '#345920', title: 'Green' },
    { color: '#16504b', title: 'Blue-green' },
    { color: '#2d555e', title: 'Blue' },
    { color: '#1e3a5f', title: 'Dark-blue' },
    { color: '#42275e', title: 'Violet' },
    { color: '#5b2245', title: 'Pink' },
    { color: '#442f19', title: 'Brown' },
    { color: '#3c3f43', title: 'Grey' },
  ];

  public lightPalette = [
    { color: '#ffffff', title: 'Default' },
    { color: '#f28b82', title: 'Red' },
    { color: '#fbbc04', title: 'Orange' },
    { color: '#fff475', title: 'Yellow' },
    { color: '#ccff90', title: 'Green' },
    { color: '#a7ffeb', title: 'Blue-green' },
    { color: '#cbf0f8', title: 'Blue' },
    { color: '#aecbfa', title: 'Dark-blue' },
    { color: '#d7aefb', title: 'Violet' },
    { color: '#fdcfe8', title: 'Pink' },
    { color: '#e6c9a8', title: 'Brown' },
    { color: '#e8eaed', title: 'Grey' },
  ];

  public palette: { color: string; title: string }[];

  public colors: string[];

  constructor(private readonly themeService: ThemeService) {
    this.themeService.getChangingThemeSubject().subscribe((isDarkTheme) => {
      this.palette = isDarkTheme ? this.blackPalette : this.lightPalette;
      this.colors = this.palette.map((el) => el.color);
    });
  }

  public getColorHexFromPalletByColorTitle(
    colorTitle: string | undefined
  ): string | undefined {
    return this.palette.find((el) => el.title === colorTitle)?.color;
  }

  public getColorTitleFromPalletByColorHex(
    colorHex: string | undefined
  ): string | undefined {
    return this.palette.find((el) => el.color === colorHex)?.title;
  }
}
