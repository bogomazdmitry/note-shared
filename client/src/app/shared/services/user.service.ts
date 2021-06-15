import { Injectable } from '@angular/core';
import { fieldLocalStorage } from '../constants/local-storage.constants';
import { User } from '../models/user.model';
import { UserDataService } from './user.data.service';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private readonly userDataService: UserDataService) {}

  public saveUser(userInfo: User) {
    localStorage.setItem(fieldLocalStorage.user, JSON.stringify(userInfo));
  }

  public getUser(): User | null {
    let userInfo: string | null = localStorage.getItem(
      fieldLocalStorage.user
      );
      return userInfo ? JSON.parse(userInfo) : null;
    }

    public getUserInfoAndSave() {
      return this.userDataService.getUserInfo().subscribe((answer) => {
        this.saveUser(answer);
    });
  }
}
