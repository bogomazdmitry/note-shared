import { Overlay } from '@angular/cdk/overlay';
import { Component, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Note } from 'src/app/shared/models/note.model';
import { NoteService } from 'src/app/shared/services/note.service';
import { NoteDialogComponent } from '../note-dialog/note-dialog.component';

@Component({
  selector: 'notes-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.scss'],
})
export class NoteComponent {
  @Input()
  public note: Note;

  @Input()
  public deleteNote: (note: Note) => void;

  private dialogIsOpened = false;

  constructor(
    public dialog: MatDialog,
    public overlay: Overlay,
    private readonly noteService: NoteService
  ) {}

  private mousePosition = {
    x: 0,
    y: 0,
  };

  public onMouseDown($event: any): void {
    this.mousePosition.x = $event.screenX;
    this.mousePosition.y = $event.screenY;
  }

  public openDialog($event: any): void {
    if (
      !this.dialogIsOpened &&
      this.mousePosition.x === $event.screenX &&
      this.mousePosition.y === $event.screenY
    ) {
      this.dialogIsOpened = true;
      const dialogRef = this.dialog.open(NoteDialogComponent, {
        data: this.note,
        disableClose: true,
        hasBackdrop: false,
        height: '500px',
        width: '780px',
        panelClass: 'note-dialog-container',
        scrollStrategy: this.overlay.scrollStrategies.noop(),
      });

      dialogRef.afterClosed().subscribe((result) => {
        this.dialogIsOpened = false;
        if (result) {
          this.noteService.changeNote(result).subscribe((updatedNote) => {
            this.note = updatedNote;
          });
        } else {
          this.deleteNote(this.note);
        }
      });
    }
  }
}
