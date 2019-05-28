import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Note, NoteMapper } from '../models/note';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { AuthenticationService } from './authentication.service';
import { ToasterCustomService } from './toaster.service';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Router } from '@angular/router';
import { isUndefined, isNull, isNullOrUndefined } from 'util';

@Injectable()
export class NotesService {

  notes: Array<Note>;
  notesSubject: BehaviorSubject<Array<Note>>;
  token: any;
  username:string;
  delvalue:boolean;
  myAppUrl: string = "http://localhost:5000/";


  constructor(private http: HttpClient, private authService: AuthenticationService,private router: Router, private toasterService: ToasterCustomService) {
    this.notes = [];
    this.notesSubject = new BehaviorSubject(this.notes);
    this.token = this.authService.getBearerToken();
    this.username = this.authService.getUserName();
  }


  fetchNotesFromServer() {
    // to fetch the notes from server
    this.username = this.authService.getUserName();
    //console.log('username: '+ this.username);
    //console.log('token: '+ this.token);
    return this.http.get<Array<Note>>(this.myAppUrl + `api/Notes/GetNotes/${this.username}`,{
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    })
    .subscribe( notes => {
      //console.log('notes' + notes);
      this.notes = notes;
      this.notesSubject.next(this.notes);
    },
    error => {
      this.toasterService.showError(error.error==null ? error.message : error.error);
      return Observable.throw(error);
    });
  }

  getNotes(): BehaviorSubject<Array<Note>> {
    this.fetchNotesFromServer();
    return this.notesSubject;
  }

  clearNotes() {
     this.notes=[];
  }

  getMaxNoteId(): Number {
    if(this.notes.length>0)
    {
      return Math.max.apply(Math,this.notes.map(function(id){return id.id})) + 1;
    }
    else{
      return 1;
    }
    }

  addNote(note: Note): Observable<Note> {
    note.id=this.getMaxNoteId();

    console.log('note: ' + note);
    return this.http.post<Note>(this.myAppUrl + `api/Notes/AddNote/${this.username}`, note, {
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
    })
    .do (addNote => {
     this.notes.push(addNote);
      this.notesSubject.next(this.notes);
      this.fetchNotesFromServer();
      })
    .catch(err => {
      return Observable.throw(err);
    });
  }

  editNote(note: Note): Observable<Note> {
    return this.http.put<Note>(this.myAppUrl + `api/Notes/${note.id}/${this.username}`, note, {
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
  }
    ).do(editNote => {
      // Update the edited notes by comparing the noteId
      const noteValue = this.notes.find(notes => notes.id === editNote.id);
      Object.assign(noteValue, editNote);
      this.notesSubject.next(this.notes);
      this.fetchNotesFromServer();
    });
  }

  deleteNote(noteId: Number): Observable<Note> {
    return this.http.delete<Note>(this.myAppUrl + `api/Notes/${this.username}/${noteId}`,{
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
      // ,params: new HttpParams()
      // .set('userid',`${this.username}`)
      // .set('id',noteId.toString())
    }
    ).do(
     delvalue => {
      // Update the edited notes by comparing the noteId
      if(delvalue){
        this.fetchNotesFromServer();
      }
    })
    .catch(err => {
      return Observable.throw(err);
    });
  }

  getNoteById(noteId): Note {
    // Get the note details by passing the noteId
    const noteValue = this.notes.find(note => note.id === noteId);
    return Object.assign({}, noteValue);
  }

    getNotesById(noteId): Observable<Note> {
    return this.http.get<Note>(this.myAppUrl + `api/Notes/GetNotesById/${this.username}/${noteId}`,{
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    });
  }
}
// import { Injectable, Inject } from '@angular/core';
// import { Observable } from 'rxjs/Observable';
// import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { Note } from '../models/note';
// import { AuthenticationService } from './authentication.service';

// @Injectable()
// export class NotesService {
//   myAppUrl: string = "http://localhost:5000/";
//   token: any;
//   username:string;

//   constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string, private _authService: AuthenticationService) {
//     //this.myAppUrl = baseUrl;
//     this.token = this._authService.getBearerToken();
//     this.username = this._authService.getUserName();
//   }

  
//   getNotes(): Observable<Array<Note>> {
//     this.username = this._authService.getUserName();
//     console.log('username: '+ this.username);
//     console.log('token: '+ this.token);
//     return this._http.get<Array<Note>>(this.myAppUrl + `api/Notes/GetNotes/${this.username}`,{
//       headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
//     });
//   }

//   getNotesById(noteId): Observable<Note> {
//     return this._http.get<Note>(this.myAppUrl + `api/Notes/GetNotesById/${this.username}/${noteId}`,{
//       headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
//     });
//   }

//   addNote(note: Note): Observable<Array<Note>> {
//     return this._http.post<Array<Note>>(this.myAppUrl + `api/Notes/AddNote/${this.username}`, note,{
//       headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
//     });
//   }
    
//   editNote(note: Note): Observable<Note> {
//     console.log('note.id ' + note.id)

//     return this._http.put<Note>(this.myAppUrl + `api/Notes/${note.id}/${this.username}`, note,{
//       headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
//     });
//   }

//   deleteNote(noteId: Number): Observable<boolean> {
//     return this._http.delete<boolean>(this.myAppUrl + `api/Notes/DeleteNote/${noteId}`,{
//       headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
//     });
//   }
// } 
