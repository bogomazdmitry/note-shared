import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { NoteComponent } from './note/note.component';
import { NotesComponent } from './notes.component';
import { NoteDialogComponent } from './note-dialog/note-dialog.component';
import { DomChangeDirective } from './note-dialog/dom-changes.directive';

@NgModule({
  declarations: [
    NoteComponent,
    NotesComponent,
    NoteDialogComponent,
    DomChangeDirective,
  ],
  imports: [CommonModule, SharedModule],
  entryComponents: [NoteDialogComponent],
})
export class NotesModule {}
