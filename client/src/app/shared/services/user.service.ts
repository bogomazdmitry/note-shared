import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { fieldLocalStorage } from '../constants/local-storage.constants';
import { User } from '../models/user.model';
import { UserDataService } from './user.data.service';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private readonly userDataService: UserDataService) {}

  public saveUser(userInfo: User): void {
    localStorage.setItem(fieldLocalStorage.user, JSON.stringify(userInfo));
  }

  public getUser(): User | null {
    const userInfo = localStorage.getItem(fieldLocalStorage.user);
    return userInfo ? JSON.parse(userInfo) : null;
  }

  public getUserInfoAndSave(): Subscription {
    return this.userDataService.getUserInfo().subscribe((answer) => {
      this.saveUser(answer);
    });
  }
}
