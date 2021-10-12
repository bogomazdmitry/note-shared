import { AuthService } from 'src/app/shared/services/auth.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { hubsRoutes } from './../constants/url.constants';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { Note } from '../models/note.model';
import { NoteText } from '../models/note-text.model';

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

  public updateNoteText(noteText: NoteText): Promise<NoteText> {
    return this.hubConnection.invoke('UpdateNoteText', noteText);
  }

  public connectToUpdateNotes(): BehaviorSubject<NoteText | null> {
    const noteBehaviorSubject = new BehaviorSubject<NoteText | null>(null);
    this.hubConnection.on('UpdateNoteText', (data) => {
      noteBehaviorSubject.next(data);
    });
    return noteBehaviorSubject;
  }

  public disconnectToUpdateNote(): void {
    this.hubConnection.off('UpdateNoteResponse');
  }
}
