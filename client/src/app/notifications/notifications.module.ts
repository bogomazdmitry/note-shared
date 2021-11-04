import { DynamicNotificationsComponent } from './dynamic-notifications/dynamic-notifications.component';
import { MenuNotificationDirective } from './menu-notification.directive';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsComponent } from './notifications.component';
import { SharedModule } from '../shared/shared.module';
import { ClickStopPropagationDirective } from './click-stop-propagation.directive';
import { DeclinedRequestSharedNoteComponent } from './declined-request-shared-note/declined-request-shared-note.component';
import { RequestSharedNoteComponent } from './request-shared-note/request-shared-note.component';
import { AcceptedRequestSharedNoteComponent } from './accepted-request-shared-note/accepted-request-shared-note.component';

@NgModule({
  declarations: [
    NotificationsComponent,
    MenuNotificationDirective,
    ClickStopPropagationDirective,
    DeclinedRequestSharedNoteComponent,
    DynamicNotificationsComponent,
    RequestSharedNoteComponent,
    AcceptedRequestSharedNoteComponent,
  ],
  exports: [NotificationsComponent],
  entryComponents: [
    DeclinedRequestSharedNoteComponent,
    RequestSharedNoteComponent,
    AcceptedRequestSharedNoteComponent,
  ],
  imports: [CommonModule, SharedModule],
})
export class NotificationsModule {}
