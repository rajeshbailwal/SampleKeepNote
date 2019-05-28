import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Reminder } from '../models/reminder';

import { AuthenticationService } from './authentication.service';
import { ToasterCustomService } from './toaster.service';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Router } from '@angular/router';

@Injectable()
export class ReminderService {

  reminders: Array<Reminder>;
  token: any;
  username: string;
  delvalue: boolean;
  myAppUrl: string = "http://localhost:5004/";

  constructor(private http: HttpClient, private authService: AuthenticationService, private router: Router, private toasterService: ToasterCustomService) {
    this.reminders = [];
    this.token = this.authService.getBearerToken();
    this.username = this.authService.getUserName();
  }

  fetchRemindersFromServer(): Observable<Array<Reminder>> {
    // to fetch the notes from server
    this.username = this.authService.getUserName();
    //console.log('username: ' + this.username);
    //console.log('token: ' + this.token);
    //console.log('Reminder: fetchRemindersFromServer ' );
    return this.http.get<Array<Reminder>>(this.myAppUrl + `api/Reminder/GetReminders/${this.username}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    });
  }

  fetchAllRemindersFromServer(): Observable<Array<Reminder>> {
    return this.http.get<Array<Reminder>>(this.myAppUrl + `api/Reminder/GetReminders`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    });
  }


  getReminderByName(reminderName: string): boolean {
    let record = null;
    if (this.reminders.length > 0) {
      record = this.reminders.find(i => i.name.toLowerCase() === reminderName.toLowerCase());
    }

    if (record != null) {
      return true;
    }
    return false;
  }

  addReminder(reminder: Reminder): Observable<Reminder> {

    reminder.createdBy=this.username;

    //console.log('Reminder: addReminder ' );
    return this.http.post<Reminder>(this.myAppUrl + `api/Reminder/AddReminder/${this.username}`, reminder, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${this.token}`)
    })
      .do(addReminder => {
        this.reminders.push(addReminder);
        this.fetchRemindersFromServer();
      })
      .catch(err => {
        return Observable.throw(err);
      });
  }

  deleteReminder(reminderId: Number): Observable<Reminder> {
    return this.http.delete<Reminder>(this.myAppUrl + `api/Reminder/DeleteReminder/${reminderId}`, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${this.token}`)
      // ,params: new HttpParams()
      // .set('userid',`${this.username}`)
      // .set('id',noteId.toString())
    }
    ).do(
      delvalue => {
        // Update the edited notes by comparing the noteId
        if (delvalue) {
        }
      })
      .catch(err => {
        return Observable.throw(err);
      });
  }

}
