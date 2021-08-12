import { MuuriService } from './../../shared/services/muuri.service';
import { Overlay } from '@angular/cdk/overlay';
import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Note } from 'src/app/shared/models/note.model';
import { NoteService } from 'src/app/shared/services/note.service';
import { NoteSignalRService } from 'src/app/shared/services/note.signalr.service';
import { NoteDialogComponent } from '../note-dialog/note-dialog.component';

@Component({
  selector: 'notes-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.scss'],
})
export class NoteComponent {
  @Input()
  public note: Note;

  private dialogIsOpened = false;

  private mousePosition = {
    x: 0,
    y: 0,
  };

  constructor(
    public dialog: MatDialog,
    public overlay: Overlay,
    private readonly noteService: NoteService,
    private readonly muuriService: MuuriService
  ) {}

  public onMouseDown($event: any): void {
    this.mousePosition.x = $event.screenX;
    this.mousePosition.y = $event.screenY;
  }

  public onMouseUp($event: any): void {
    if (
      this.mousePosition.x !== $event.screenX &&
      this.mousePosition.y !== $event.screenY
    ) {
      this.muuriService.updateOrder();
    }
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
      const sub = dialogRef.afterClosed().subscribe((result) => {
        this.dialogIsOpened = false;
        if (result) {
          this.noteService.updateNote(result);
        } else {
          this.noteService.deleteNote(this.note.id);
        }
        sub.unsubscribe();
      });
    }
  }
}
