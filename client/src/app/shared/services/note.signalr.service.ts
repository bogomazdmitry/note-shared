import { AuthService } from 'src/app/shared/services/auth.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { actionRoutes, hubMethodSubscription, hubsRoutes } from './../constants/url.constants';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { NoteText } from '../models/note-text.model';
import { from } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class NoteSignalRService {
  private hubConnection: signalR.HubConnection;

  constructor(private readonly authService: AuthService) {}

  public startConnection(): void {
    const options: signalR.IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return this.authService.getAccessToken() ?? '';
      },
    };

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.serverUrl + hubsRoutes.note, options)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connected to hub'))
      .catch((error) => console.log('No connection to hub ' + error));
  }

  public updateNoteText(noteText: NoteText): Observable<NoteText> {
    return from(this.hubConnection.invoke(actionRoutes.noteTextUpdate, noteText));
  }

  public shareNoteWithUser(email: string, noteTextID: number): Observable<any> {
    return from(this.hubConnection.invoke(actionRoutes.requestSharedNote, { email, noteTextID }));
  }

  public connectToUpdateNotes(): BehaviorSubject<NoteText | null> {
    const noteBehaviorSubject = new BehaviorSubject<NoteText | null>(null);
    this.hubConnection.on(hubMethodSubscription.noteTextUpdate, (data) => {
      noteBehaviorSubject.next(data);
    });
    return noteBehaviorSubject;
  }

  public disconnectToUpdateNote(): void {
    this.hubConnection.off(hubMethodSubscription.noteTextUpdate);
  }

  public declineSharedNote(noteTextID: number, notificationID: number): Observable<any> {
    return from(this.hubConnection.invoke(actionRoutes.declineSharedNote, noteTextID, notificationID));
  }

  public acceptSharedNote(noteTextID: number, notificationID: number): Observable<any> {
    return from(this.hubConnection.invoke(actionRoutes.acceptSharedNote, noteTextID, notificationID));
  }
}
