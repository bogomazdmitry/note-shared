import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class UnAuthGuard implements CanActivate {
  constructor(private authService: AuthService) {}

  public canActivate(): boolean {
    return !this.authService.isAuthorize();
  }
}
