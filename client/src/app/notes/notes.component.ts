import { MuuriService } from './../shared/services/muuri.service';
import {
  Component,
  ElementRef,
  ViewChildren,
  OnInit,
  AfterViewInit,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import { NoteService } from '../shared/services/note.service';

@Component({
  selector: 'notes-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.scss'],
})
export class NotesComponent implements AfterViewInit, OnDestroy {
  @ViewChild('gridElement', { read: ElementRef })
  public gridElement: ElementRef;

  constructor(
    public readonly noteService: NoteService,
    public readonly muuriService: MuuriService
  ) {}

  public ngAfterViewInit(): void {
    this.muuriService.setGridElement(this.gridElement);
    if (this.gridElement.nativeElement.childElementCount > 0) {
      this.muuriService.initGrid();
    } else {
      this.muuriService.startInitMutation(
        this.muuriService.initGrid.bind(this.muuriService)
      );
    }
  }

  public ngOnDestroy(): void {
    this.muuriService.refreshNotesElementMutation?.disconnect();
    this.muuriService.addNotesElementMutation?.disconnect();
  }
}
