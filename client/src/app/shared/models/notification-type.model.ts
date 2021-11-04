import { Type } from "@angular/core";
import { AcceptedRequestSharedNoteComponent } from "src/app/notifications/accepted-request-shared-note/accepted-request-shared-note.component";
import { BaseNotification } from "src/app/notifications/base-notification/base-notification";
import { DeclinedRequestSharedNoteComponent } from "src/app/notifications/declined-request-shared-note/declined-request-shared-note.component";
import { RequestSharedNoteComponent } from "src/app/notifications/request-shared-note/request-shared-note.component";

export enum NotificationType {
  RequestSharedNoteType = 0,
  AcceptedRequestSharedNoteType = 1,
  DeclinedRequestSharedNoteType = 2,
}

export const converter = new Map<NotificationType, Type<BaseNotification>>([
  [NotificationType.RequestSharedNoteType, RequestSharedNoteComponent],
  [NotificationType.AcceptedRequestSharedNoteType, AcceptedRequestSharedNoteComponent],
  [NotificationType.DeclinedRequestSharedNoteType, DeclinedRequestSharedNoteComponent],
]);
