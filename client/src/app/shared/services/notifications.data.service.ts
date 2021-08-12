import { BaseDataService } from './base.data.service';
import { Injectable } from '@angular/core';
import { BaseNotification } from '../models/base-notification.model';
import { NoteNotificationComponent } from 'src/app/notifications/note-notification/note-notification.component';
import { controllerRoutes } from '../constants/url.constants';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class NotificationsDataService extends BaseDataService {
  constructor(protected readonly httpClient: HttpClient) {
    super(httpClient, controllerRoutes.notifications);
  }

  public getNoteNotification(): void {}
}
