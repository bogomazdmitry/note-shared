import { Component, Input, OnInit } from '@angular/core';
import { NotificationConfigure } from 'src/app/shared/models/notification-configure.model';

@Component({
  selector: 'template-notification-notification',
  templateUrl: './template-notification.component.html',
  styleUrls: ['./template-notification.component.scss'],
})
export class NoteNotificationComponent {
  @Input()
  public notificationConfigure: NotificationConfigure;
}
