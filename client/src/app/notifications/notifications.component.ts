import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';

@Component({
  selector: 'notifications-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss'],
})
export class NotificationsComponent implements OnInit, AfterViewInit {
  public countNotifications = 0;

  public constructor(private readonly changeDetectorRef: ChangeDetectorRef) {}

  public ngOnInit(): void {}

  public ngAfterViewInit(): void {
    this.changeDetectorRef.detectChanges();
  }

  public setCountNotification(event: number): void {
    this.countNotifications = event;
  }
}
