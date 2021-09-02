import { MenuNotificationDirective } from './menu-notification.directive';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsComponent } from './notifications.component';
import { NoteNotificationComponent } from './template-notification/template-notification.component';
import { SharedModule } from '../shared/shared.module';
import { ClickStopPropagationDirective } from './click-stop-propagation.directive';

@NgModule({
  declarations: [
    NotificationsComponent,
    NoteNotificationComponent,
    MenuNotificationDirective,
    ClickStopPropagationDirective,
  ],
  exports: [NotificationsComponent],
  entryComponents: [NoteNotificationComponent],
  imports: [CommonModule, SharedModule],
})
export class NotificationsModule {}
