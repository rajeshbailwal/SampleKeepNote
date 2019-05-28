import { Component, Input, EventEmitter, Output } from '@angular/core';
import { Note } from '../../models/note';
import { RouterService } from '../../services/router.service';

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent {
  @Input()
  note: Note;

  @Output() deleteEvent = new EventEmitter<string>();

  constructor(private routerService: RouterService) { }

  openEditView() {
    //console.log(this.note.id);
    this.routerService.routeToEditNoteView(this.note.id);
    //console.log(this.note);
  }

  deleteNote(event, noteId) {
    //console.log(noteId);
    this.deleteEvent.emit(noteId);
  }

}
