import { Note } from "./note.model";

export interface NoteDesign {
  id: number;
  color: string;
  notes: Note[] | undefined;
}
