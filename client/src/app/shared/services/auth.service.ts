import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { fieldLocalStorage } from '../constants/local-storage.constants';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private jwtHelperService: JwtHelperService;

  constructor(private readonly router: Router) {
    this.jwtHelperService = new JwtHelperService();
  }

  public isAuthorize(): boolean {
    const token = localStorage.getItem(fieldLocalStorage.accessToken);
    return token != null && !this.jwtHelperService.isTokenExpired(token);
  }

  public saveAccessToken(token: any): void {
    localStorage.setItem(fieldLocalStorage.accessToken, token.access_token);
  }

  public getAccessToken() {
    return localStorage.getItem(fieldLocalStorage.accessToken);
  }

  public signOut(): void {
    localStorage.removeItem(fieldLocalStorage.accessToken);
    localStorage.removeItem(fieldLocalStorage.user);
    this.router.navigate(['/']);
  }
}
