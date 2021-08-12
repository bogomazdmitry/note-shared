import { Component, Input, OnInit } from '@angular/core';
import { BaseNotification } from 'src/app/shared/models/base-notification.model';
import { BaseNotificationComponent } from '../base-notification/base-notification.component';

@Component({
  selector: 'notifications-note-notification',
  templateUrl: './note-notification.component.html',
  styleUrls: ['./note-notification.component.scss']
})
export class NoteNotificationComponent implements OnInit, BaseNotificationComponent {
  @Input()
  public notification: any;

  constructor() { }

  public ngOnInit(): void {
  }

}
