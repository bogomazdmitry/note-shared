import { ThemeService } from './../../shared/services/theme.service';
import { ThemeChangerComponent } from './../../shared/layout/theme-changer/theme-changer.component';
import { ColorPaletteService } from './../../shared/services/color-palette.service';
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
import { MatChipInputEvent } from '@angular/material/chips';
import { ColorEvent } from 'ngx-color';
import { Subject } from 'rxjs/internal/Subject';

@Component({
  selector: 'notes-note-dialog',
  templateUrl: 'note-dialog.component.html',
})
export class NoteDialogComponent implements OnInit {
  public userEmails: string[] = [];

  constructor(
    public dialogReference: MatDialogRef<NoteDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public note: Note,
    private readonly noteService: NoteService,
    private readonly userService: UserService,
    public readonly colorPaletteService: ColorPaletteService,
    public readonly themeService: ThemeService
  ) {
    if (!this.note.noteText) {
      this.note.noteText = { id: -1, title: '', text: '' };
    }
  }

  public ngOnInit(): void {
    this.noteService
      .getSharedUsersEmails(this.note.noteText.id)
      .subscribe((result) => {
        const currentEmail = this.userService.getUser()?.email;
        this.userEmails = result.filter((e) => e !== currentEmail);
      });

    // subscribe to changing theme
    this.themeService.getChangingThemeSubject().subscribe((isDarkTheme) => {
      this.note.hexColor =
        this.colorPaletteService.getColorHexFromPalletByColorTitle(
          this.note.noteDesign?.color
        );
    });

    // using js dom
    const dialog = document.getElementById(this.dialogReference.id);
    if (dialog) {
      dialog.addEventListener('mousedown', () => {
        const parent = dialog.parentElement?.parentElement?.parentElement;
        const child = dialog.parentElement?.parentElement;
        if (parent && child && parent.lastElementChild !== child) {
          parent.appendChild(child);
        }
      });
    }
  }

  public saveNote(): void {
    this.noteService.updateNoteText(this.note.noteText).subscribe();
    this.dialogReference.close(this.note);
  }

  public deleteNote(): void {
    this.dialogReference.close(null);
  }

  public changeColorHandler($event: ColorEvent): void {
    const newColorTitle =
      this.colorPaletteService.getColorTitleFromPalletByColorHex(
        $event.color.hex
      );
    if (newColorTitle) {
      this.note.hexColor = $event.color.hex;
      if (!this.note.noteDesign) {
        this.note.noteDesign = { color: newColorTitle };
      } else {
        this.note.noteDesign.color = newColorTitle;
      }
      this.noteService
        .updateNoteDesign(this.note.noteDesign, this.note.id)
        .subscribe((result) => {
          this.note.noteDesign = result;
        });
    }
  }

  public addUser($event: MatChipInputEvent): void {
    this.noteService
      .shareNoteWithUser($event.value, this.note.noteText.id)
      .subscribe((result) => {
        console.log(result);
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
