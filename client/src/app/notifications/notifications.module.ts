import { MenuNotificationDirective } from './menu-notification.directive';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsComponent } from './notifications.component';
import { NoteNotificationComponent } from './note-notification/note-notification.component';
import { SharedModule } from '../shared/shared.module';
import { DynamicNotificationsComponent } from './dynamic-notifications/dynamic-notifications.component';

@NgModule({
  declarations: [
    NotificationsComponent,
    NoteNotificationComponent,
    MenuNotificationDirective,
    DynamicNotificationsComponent,
  ],
  exports: [NotificationsComponent],
  entryComponents: [NoteNotificationComponent],
  imports: [CommonModule, SharedModule],
})
export class NotificationsModule {}
