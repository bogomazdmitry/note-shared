import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Note } from '../models/note.model';
import { NoteOrder } from '../models/notes-order.model';
import { NoteDataService } from './note.data.service';
import { NotesDataService } from './notes.data.service';

@Injectable({ providedIn: 'root' })
export class NoteService {
  constructor(
    protected readonly httpClient: HttpClient,
    protected readonly noteDataService: NoteDataService,
    protected readonly notesDataService: NotesDataService
  ) {}

  public changeNote(note: Note): Observable<Note> {
    return this.noteDataService.changeNote(note);
  }

  public deleteNote(id: number): Observable<any> {
    return this.noteDataService.deleteNote(id);
  }
  public createNote(order: number): Observable<Note> {
    return this.noteDataService.createNote(order);
  }

  public getNotes(): Observable<Note[]> {
    return this.notesDataService.getNotes();
  }

  public updateOrder(notesOrder: NoteOrder[]): void {
    this.notesDataService.updateOrder(notesOrder);
  }
}
