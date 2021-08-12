import {
  Component,
  ComponentFactoryResolver,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { BaseNotification } from 'src/app/shared/models/base-notification.model';
import { NotificationsService } from 'src/app/shared/services/notifications.service';
import { BaseNotificationComponent } from '../base-notification/base-notification.component';
import { MenuNotificationDirective } from '../menu-notification.directive';

@Component({
  selector: 'notifications-dynamic-notifications',
  templateUrl: './dynamic-notifications.component.html',
  styleUrls: ['./dynamic-notifications.component.scss'],
})
export class DynamicNotificationsComponent implements OnInit {
  public notifications: BaseNotification[];
  @ViewChild(MenuNotificationDirective, { static: true })
  public menuNotification!: MenuNotificationDirective;
  @Output()
  public changingCountNotificationsEvent = new EventEmitter<number>();

  constructor(
    private readonly notificationsService: NotificationsService,
    private componentFactoryResolver: ComponentFactoryResolver
  ) {
    this.notifications = this.notificationsService.notifications;
  }
  public ngOnInit(): void {
    this.loadNotifications();
  }
  public loadNotifications(): void {
    const viewContainerRef = this.menuNotification.viewContainerRef;
    viewContainerRef.clear();
    this.notifications.forEach((adItem) => {
      const componentFactory =
        this.componentFactoryResolver.resolveComponentFactory(adItem.component);

      const componentRef =
        viewContainerRef.createComponent<BaseNotificationComponent>(
          componentFactory
        );
      componentRef.instance.notification = adItem.notification;
    });
    this.changingCountNotificationsEvent.emit(this.notifications.length);
  }
}
