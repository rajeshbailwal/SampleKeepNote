import { Component, OnInit, Inject } from '@angular/core';
import { Note } from '../../models/note';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NotesService } from '../../services/notes.service';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material';
import { ToasterCustomService } from '../../services/toaster.service';
import { TagService } from '../../services/tag.service';
import { Tag } from '../../models/tag';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ReminderService } from '../../services/reminder.service';
import { Reminder } from '../../models/reminder';

@Component({
  selector: 'app-edit-note-view',
  templateUrl: './edit-note-view.component.html',
  styleUrls: ['./edit-note-view.component.css']
})
export class EditNoteViewComponent implements OnInit {
  note: Note;
  states: Array<string> = ['not-started', 'started', 'completed'];
  errMessage: string;
  tagWarning: boolean;
  tags: Array<Tag> = [];
  reminders: Array<Reminder> = [];
  tagText: string;
  reminderText: string;
  myControl = new FormControl();
  myControlReminder = new FormControl();
  filteredOptions: Observable<string[]>;
  filteredOptionsReminders: Observable<string[]>;

  // edit noteview component used to display the popup with preloaded values 
  constructor(private dialogRef: MatDialogRef<EditNoteViewComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private noteService: NotesService, private toasterService: ToasterCustomService
    , private tagService: TagService
    , private reminderService: ReminderService) { }

  ngOnInit() {
    this.note = new Note();
    this.tagText = '';
    this.reminderText = '';
    //this.note = this.noteService.getNoteById(parseInt(this.data.noteId, 0));
    //console.log('note id:' + parseInt(this.data.noteId, 0));
    this.noteService.getNotesById(parseInt(this.data.noteId, 0)).subscribe(
      data => {
        this.note = data;
        if(this.note.reminders !=null)
        {
          this.reminderText=this.note.reminders[0].name;
        }

        if(this.note.category !=null)
        {
        this.tagText=this.note.category.name;
        }
        this.getAllTags();
        this.getAllReminders();
       
        this.filterOptionsTags();
      },
      error => {
        this.toasterService.showError(error.error==null ? error.message : error.error);
      }
    );
  }

  private filterOptionsTags() {
    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }
  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    let records = this.tags.map(x => x.name).filter(name => name.toLowerCase().includes(filterValue));

    return records;
  }
  private filterOptionsReminders() {
    this.filteredOptionsReminders = this.myControlReminder.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filterReminder(value))
      );
  }

  private _filterReminder(value: string): string[] {
    const filterValue = value.toLowerCase();

    let records = this.reminders.map(x => x.name).filter(name => name.toLowerCase().includes(filterValue));

    //console.log('recordsReminder' + records);
    return records;
  }

  getAllTags() {
    this.tagService.fetchAllTagsFromServer().subscribe(data => {
      this.tags = data;
      this.filterOptionsTags();
    });
  }

  getAllReminders() {
    this.reminderService.fetchAllRemindersFromServer().subscribe(data => {
      this.reminders = data;
      //console.log('reminders : ' + this.reminders);
      this.filterOptionsReminders();
    });
  }

  onSave() {

    if (this.tagText != '') {
      let record = this.tags.find(({ name }) => name == this.tagText);
      //console.log('record :' + record);
      if (record == null) {
        this.errMessage = this.errMessage + "Invalid Tag value" + '<br/>';
      }
      else {
        this.note.category= new Tag();
        this.note.category = record;
      }
    }
    else
    {
      this.note.category=null;
    }

    if (this.reminderText != '') {
      let record = this.reminders.find(({ name }) => name == this.reminderText);
      //console.log('record :' + record);
      if (record == null) {
        this.errMessage = this.errMessage + "Invalid Reminder value" + '<br/>';
      }
      else {
        this.note.reminders= new Array<Reminder>();
        this.note.reminders.push(record);
      }
    }
    else
    {
      this.note.reminders=null;
    }

    this.noteService.editNote(this.note).subscribe(
      editNote => {
        this.toasterService.showSuccess('Note edited successfully.');
        this.dialogRef.close();
      },
      error => {
        this.toasterService.showError(error.error==null ? error.message : error.error);
      }
    );
  }

  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;
    let addTag = true;

    if (this.note.tags == null) {
      this.note.tags = new Array<string>();
    }

    const index = this.note.tags.indexOf(value);
    if (index >= 0 || this.note.tags.length === 5) {
      addTag = false;
    }

    if (addTag) {
      // Add our fruit
      if ((value || '').trim()) {
        this.note.tags.push(value.trim());
      }
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(tag: string): void {
    const index = this.note.tags.indexOf(tag);

    if (index >= 0) {
      this.note.tags.splice(index, 1);
    }
  }
}
