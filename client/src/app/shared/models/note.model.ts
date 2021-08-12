import { NoteDesign } from './note-design.model';
import { NoteHistory } from './note-history.model';
import { NoteText } from './note-text.model';

export interface Note {
  id: number;
  number: number;
  order: number;
  noteDesign: NoteDesign | undefined;
  noteHistory: NoteHistory | undefined;
  noteText: NoteText;
}
