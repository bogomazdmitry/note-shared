import { NoteDesign } from './note-design.model';
import { NoteHistory } from './note-history.model';
import { User } from './user.model';

export interface Note {
  id: number;
  order: number;
  tittle: string;
  text: string;
  number: number;
  designID: number;
  noteDesign: NoteDesign | undefined;
  historyID: number;
  noteHistory: NoteHistory | undefined;
  userID: string;
  user: User | undefined;
}
