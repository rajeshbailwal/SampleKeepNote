import { Component, OnInit } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { Note, NoteMapper } from '../models/note';
import { NotesService } from '../services/notes.service';
import { ToasterCustomService } from '../services/toaster.service';
import { NgForm } from '@angular/forms';
import { MatChipInputEvent } from '@angular/material';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { TagService } from '../services/tag.service';
import { Tag } from '../models/tag';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ReminderService } from '../services/reminder.service';
import { Reminder } from '../models/reminder';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']

})
export class DashboardComponent implements OnInit {

  note: Note = new Note();
  notes: Array<Note> = [];
  errMessage: string;
  alertmessage: string;
  alertType: string;
  tags: Array<Tag> = [];
  reminders: Array<Reminder> = [];
  tagText: string;
  reminderText: string;
  myControl = new FormControl();
  myControlReminder = new FormControl();
  filteredOptions: Observable<string[]>;
  filteredOptionsReminders: Observable<string[]>;

  constructor(private notesService: NotesService, private toasterService: ToasterCustomService, private tagService: TagService
    , private reminderService: ReminderService) { }

  ngOnInit() {
    this.getNotes();
    this.getAllTags();
    this.getAllReminders();
    this.tagText = '';
    this.reminderText = '';
    this.filterOptionsTags();
    //this.filterOptionsReminders();
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

    console.log('recordsReminder' + records);
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
      console.log('reminders : ' + this.reminders);
      this.filterOptionsReminders();
    });
  }

  getNotes() {
    this.notesService.getNotes().subscribe(
      data => {
        this.notes = data; console.log('data:' + data)
      },
      error => {
        //this.errMessage = error.message;
        console.log('errr:' + error)
        this.toasterService.showError(error.error==null ? error.message : error.error);
      }
    );

  }

  takeNotes(form: NgForm) {
    this.errMessage = '';
    const formData = this.note;
    event.preventDefault();
    //let record: Tag = new Tag();


    this.tagText = this.tagText.trim();

    console.log('tag: ' + this.tagText);

    console.log(formData);
    if (!formData.title || formData.title.length === 0 || !formData.content || formData.content.length === 0) {
      this.errMessage = this.errMessage + 'Title and Text both are required fields <br/>';
    }
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

    if (this.errMessage.length > 0) {
      this.toasterService.showError(this.errMessage);
    }
    else {
      //console.log(this.note);

      // let notemap= new NoteMapper();
      // notemap.id=this.note.id;
      // notemap.title=this.note.title;
      // notemap.content=this.note.content;
      // notemap.userid=this.note.userid;
      // if(this.note.reminders !=null)
      // {
      //   notemap.reminders= Array<Reminder>();
      // notemap.reminders.push(this.note.reminders);
      // }
      // notemap.category=this.note.category;

      this.notesService.addNote(this.note).subscribe(
        data => {
          //console.log(data);
          //this.notes = this.notesService.getNotes();
          this.toasterService.showSuccess('Note added successfully.');
        },
        error => {
          if (error.status === 404) {
            this.toasterService.showError(error.error==null ? error.message : error.error);
          } else {
            this.toasterService.showError(error.error==null ? error.message : error.error);
          }
        }
      );
      this.note = new Note();
      this.reminderText='';
      this.tagText='';
      form.reset();

    }
  }

  deleteNote(noteId) {
    //console.log(noteId);
    this.notesService.deleteNote(noteId).subscribe(
      data => {
        //this.getNotes();
        this.toasterService.showSuccess('Note Deleted Successfully.');
      },
      error => {
        //console.log(JSON.stringify(error));
        this.toasterService.showError(error.error==null ? error.message : error.error);
      }
    );
  }

  checkItem(event, tagText) {
    //console.log(tagText);
    let isExist = this.tags.some(e => e.name === tagText);

    //console.log(tagText);
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
  //openEditView() {
  //  this.routerService.routeToEditNoteView(this.note.id);
  //  //console.log(this.note);
  //}

}
