import { Note } from 'src/app/shared/models/note.model';
import { Component, Inject, Input } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'note-dialog',
  templateUrl: 'note-dialog.component.html',
})
export class NoteDialogComponent {

  constructor(
    public dialogReference: MatDialogRef<NoteDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public note: Note
  ) {}

  public saveNote(): void {
    this.dialogReference.close(this.note);
  }

  public deleteNote(): void {
    this.dialogReference.close(null);
  }
}
