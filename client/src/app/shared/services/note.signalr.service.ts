import { hubsRoutes } from './../constants/url.constants';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { Note } from '../models/note.model';

@Injectable({ providedIn: 'root' })
class NoteSignalRService {
  private hubConnection: signalR.HubConnection;

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.serverUrl + hubsRoutes.note)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connected to hub'))
      .catch((error) => console.log('No connection to hub ' + error));
  }

  constructor() {}

  public updateNote(note: Note): void {
    this.hubConnection.invoke('updateNote', note);
  }

  public connectToUpdateNote(note: Note): void {
    this.hubConnection.on('updateNoteResponse', (data) => {
      console.log(data);
    });
  }

  public disconnectToUpdateNote(): void {
    this.hubConnection.off('updateNoteResponse');
  }
}
