import { Component, OnInit } from '@angular/core';
import { NotificationInfo } from 'src/app/shared/models/notification-info.model';
import { NotificationsService } from 'src/app/shared/services/notifications.service';
import { BaseNotification } from '../base-notification/base-notification';

@Component({
  selector: 'app-declined-request-shared-note',
  templateUrl: './declined-request-shared-note.component.html',
  styleUrls: ['./declined-request-shared-note.component.scss']
})
export class DeclinedRequestSharedNoteComponent implements OnInit, BaseNotification {

  constructor(private readonly notificationsService: NotificationsService) { }

  public notification: NotificationInfo;

  public ngOnInit(): void {
  }

  public deleteNotification(): void {
    this.notificationsService.deleteNotification(this.notification.id);
  }
}
