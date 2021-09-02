import { Injectable } from '@angular/core';
import { BaseNotification } from '../models/base-notification.model';
import { NoteNotificationComponent } from 'src/app/notifications/template-notification/template-notification.component';
import { NotificationsDataService } from './notifications.data.service';

@Injectable({
  providedIn: 'root',
})
export class NotificationsService {
  public notifications: BaseNotification[];

  constructor(
    private readonly notificationsDataService: NotificationsDataService
  ) {
    this.notifications = [new BaseNotification(NoteNotificationComponent, { email: 'dima@yandex.ru'})];
  }
}
