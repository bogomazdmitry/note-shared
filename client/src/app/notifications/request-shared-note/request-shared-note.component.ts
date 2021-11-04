import { Component, OnInit } from '@angular/core';
import { NotificationInfo, RequestSharedNoteNotificationContent } from 'src/app/shared/models/notification-info.model';
import { NoteService } from 'src/app/shared/services/note.service';
import { NotificationsService } from 'src/app/shared/services/notifications.service';
import { BaseNotification } from '../base-notification/base-notification';

@Component({
  selector: 'app-request-shared-note',
  templateUrl: './request-shared-note.component.html',
  styleUrls: ['./request-shared-note.component.scss']
})
export class RequestSharedNoteComponent implements OnInit, BaseNotification {

  constructor(
    private readonly notificationsService: NotificationsService,
    private readonly noteService: NoteService
  ) { }

  public notification: NotificationInfo;
  public content: RequestSharedNoteNotificationContent;

  public ngOnInit(): void {
    this.content = JSON.parse(this.notification.content);
    console.log(this.content);
    console.log(this.notification);
  }

  public declineRequest(): void {
    this.noteService.declineSharedNote(this.content.noteTextID, this.notification.id).subscribe(() => {
      this.notificationsService.deleteNotificationFromUI(this.notification.id);
    });
  }

  public acceptRequest(): void {
    this.noteService.acceptSharedNote(this.content.noteTextID, this.notification.id).subscribe((newNote) => {
      this.noteService.addNote(newNote);
      this.notificationsService.deleteNotificationFromUI(this.notification.id);
    });
  }
}
