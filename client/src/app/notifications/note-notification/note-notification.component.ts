import { Component, Input, OnInit } from '@angular/core';
import { BaseNotification } from 'src/app/shared/models/base-notification.model';
import { NotificationDynamicComponent } from '../base-notification/notification.dynamic-component';

@Component({
  selector: 'notifications-note-notification',
  templateUrl: './note-notification.component.html',
  styleUrls: ['./note-notification.component.scss'],
})
export class NoteNotificationComponent implements NotificationDynamicComponent {
  @Input()
  public notification: any;
}
