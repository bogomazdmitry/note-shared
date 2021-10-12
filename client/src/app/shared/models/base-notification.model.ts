import { Type } from '@angular/core';

export class BaseNotification {
  constructor(public component: Type<any>, public notification: any) {}
}
