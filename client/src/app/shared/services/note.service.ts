import { NoteSignalRService } from 'src/app/shared/services/note.signalr.service';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Note } from '../models/note.model';
import { NoteOrder } from '../models/notes-order.model';
import { NoteDataService } from './note.data.service';
import { NotesDataService } from './notes.data.service';
import { NoteDesign } from '../models/note-design.model';
import { NoteText } from '../models/note-text.model';

@Injectable({ providedIn: 'root' })
export class NoteService implements OnDestroy {
  public notes: Note[];

  constructor(
    protected readonly httpClient: HttpClient,
    protected readonly noteDataService: NoteDataService,
    protected readonly notesDataService: NotesDataService,
    protected readonly noteSignalR: NoteSignalRService
  ) {
    this.getNotes().subscribe((notes) => {
      this.notes = notes;
    });

    this.noteSignalR.startConnection();

    this.noteSignalR
      .connectToUpdateNotes()
      .subscribe((updatedNoteText: NoteText | null) => {
        if (updatedNoteText) {
          const indexNote = this.notes.findIndex((note) => {
            return note.noteText.id === updatedNoteText.id;
          });
          this.notes[indexNote].noteText = updatedNoteText;
        }
      });
  }

  public ngOnDestroy(): void {
    this.noteSignalR.disconnectToUpdateNote();
  }

  public updateNote(note: Note): Observable<Note> {
    const subscription = this.noteDataService.updateNote(note);
    subscription.subscribe();
    return subscription;
  }

  public updateNoteText(noteText: NoteText): Promise<NoteText> {
    return this.noteSignalR.updateNoteText(noteText);
  }

  public deleteNote(id: number): Observable<any> {
    const subscription = this.noteDataService.deleteNote(id);
    subscription.subscribe((deleteNote) => {
      this.notes = this.notes.filter((note) => note.id !== id);
    });
    return subscription;
  }

  public createNote(): Observable<Note> {
    const order = -1;
    const observable = this.noteDataService.createNote(order);
    observable.subscribe((newNote) => {
      if (!this.notes) {
        this.notes = [];
      }
      this.notes.push(newNote);
    });
    return observable;
  }

  public getNotes(): Observable<Note[]> {
    return this.notesDataService.getNotes();
  }

  public updateOrder(notesOrder: NoteOrder[]): Observable<NoteOrder[]> {
    return this.notesDataService.updateOrder(notesOrder);
  }

  public disconnectToUpdateNote(): void {
    this.noteSignalR.disconnectToUpdateNote();
  }

  public updateNoteDesign(
    noteDesign: NoteDesign,
    noteID: number
  ): Observable<NoteDesign> {
    return this.noteDataService.updateNoteDesign(noteDesign, noteID);
  }

  public getSharedUsersEmails(noteTextID: number): Observable<string[]> {
    return this.noteDataService.getSharedUsersEmails(noteTextID);
  }

  public addSharedUser(email: string, noteTextID: number): Observable<any> {
    return this.noteDataService.addSharedUser(email, noteTextID);
  }

  public deleteSharedUser(email: string, noteTextID: number): Observable<any> {
    return this.noteDataService.deleteSharedUser(email, noteTextID);
  }
}
