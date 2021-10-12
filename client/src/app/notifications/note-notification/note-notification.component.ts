import { Component, Input, OnInit } from '@angular/core';
import { BaseNotificationInterface } from '../base-notification/base-notification';

@Component({
  selector: 'notifications-note-notification',
  templateUrl: './note-notification.component.html',
  styleUrls: ['./note-notification.component.scss'],
})
export class NoteNotificationComponent implements BaseNotificationInterface {
  @Input()
  public notification: any;
}
