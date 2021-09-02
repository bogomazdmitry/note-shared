export interface NotificationConfigure {
  text: string;
  doneCallback: (event: any) => void | undefined;
  undoCallback: (event: any) => void | undefined;
}
