import {
  Component,
  ElementRef,
  ViewChildren,
  OnInit,
  AfterViewInit,
} from '@angular/core';
import { Note } from 'src/app/shared/models/note.model';
import Grid, * as Muuri from 'muuri';
import { NoteService } from '../shared/services/note.service';

@Component({
  selector: 'notes-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.scss'],
})
export class NotesComponent implements OnInit {
  @ViewChildren('notes', { read: ElementRef })
  public notesRef: any;
  public notes: Note[];
  public grid: Grid;

  constructor(protected readonly noteService: NoteService) {}

  public ngOnInit(): void {
    this.noteService.getNotes().subscribe((result) => {
      this.notes = result;
    });
  }

  public updateOrder(): void {
    this.noteService.updateOrder(this.notes);
  }

  public refreshGrid(): void {
    if (!this.grid) {
      this.grid = new Muuri.default('.grid', {
        dragEnabled: true,
        layoutOnInit: true,
      });
      let onceUpdate = true;
      this.grid.on('layoutEnd', () => {
        if (onceUpdate) {
          onceUpdate = false;
          this.refreshGrid();
        }
      });
      this.grid.on('layoutEnd', this.updateOrder.bind(this));
    } else {
      this.grid.refreshItems().layout();
    }
  }

  public addNote(): void {
    this.noteService
      .createNote(this.grid.getItems().length)
      .subscribe((result) => {
        console.log(this.grid.getItems());
        console.log(result);
        this.notes.push(result);

        setTimeout(() => {
          if (this.notesRef) {
            this.grid.add(this.notesRef.last.nativeElement, { index: 0 });
          }
        }, 150);
      });
  }

  public deleteNote(note: Note): void {
    this.noteService.deleteNote(note.id).subscribe((result) => {
      this.notes = this.notes.filter((nt) => nt.id !== note.id);
    });
  }
}
