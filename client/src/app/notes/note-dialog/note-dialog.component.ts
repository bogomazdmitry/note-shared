import { NoteService } from './../../shared/services/note.service';
import { Note } from 'src/app/shared/models/note.model';
import {
  Component,
  ElementRef,
  Inject,
  ViewChild,
  OnInit,
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'note-dialog',
  templateUrl: 'note-dialog.component.html',
})
export class NoteDialogComponent implements OnInit {
  @ViewChild('colorPalette')
  public colorPalette: ElementRef;
  public userEmails: string[] = [];

  constructor(
    public dialogReference: MatDialogRef<NoteDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public note: Note,
    private readonly noteService: NoteService,
    private readonly userService: UserService
  ) {}

  public ngOnInit(): void {
    this.noteService
      .getSharedUsersEmails(this.note.noteText.id)
      .subscribe((result) => {
        const currentEmail = this.userService.getUser()?.email;
        this.userEmails = result.filter((e) => e !== currentEmail);
      });
    this.changeDialogColor(this.note.noteDesign?.color || '');
  }

  public saveNote(): void {
    this.noteService.updateNoteText(this.note.noteText);
    this.dialogReference.close(this.note);
  }

  public deleteNote(): void {
    this.dialogReference.close(null);
  }

  public changeColorHandler($event: any): void {
    if (!this.note.noteDesign) {
      this.note.noteDesign = { color: $event.color.hex };
    } else {
      this.note.noteDesign.color = $event.color.hex;
    }
    this.changeDialogColor($event.color.hex);
    this.noteService
      .updateNoteDesign(this.note.noteDesign, this.note.id)
      .subscribe((result) => {
        this.note.noteDesign = result;
      });
  }

  public changeDialogColor(color: string): void {
    const dialog = document.getElementById(this.dialogReference.id);
    if (dialog) {
      dialog.style.backgroundColor = color;
    }
  }

  public addUser($event: any): void {
    this.noteService
      .addSharedUser($event.value, this.note.noteText.id)
      .subscribe(() => {
        this.userEmails.unshift($event.value);
      });
    $event.input.value = '';
  }

  public removeUser(userEmail: string): void {
    this.noteService
      .deleteSharedUser(userEmail, this.note.noteText.id)
      .subscribe((result) => {
        this.userEmails = this.userEmails.filter((e) => e !== userEmail);
      });
  }
}
