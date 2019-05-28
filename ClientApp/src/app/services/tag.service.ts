import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Tag } from '../models/tag';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { AuthenticationService } from './authentication.service';
import { ToasterCustomService } from './toaster.service';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Router } from '@angular/router';
import { isUndefined, isNull, isNullOrUndefined } from 'util';

@Injectable()
export class TagService {

  tags: Array<Tag>;
  token: any;
  username: string;
  delvalue: boolean;
  myAppUrl: string = "http://localhost:5002/";

  constructor(private http: HttpClient, private authService: AuthenticationService, private router: Router, private toasterService: ToasterCustomService) {
    this.tags = [];
    this.token = this.authService.getBearerToken();
    this.username = this.authService.getUserName();
  }

  fetchTagsFromServer(): Observable<Array<Tag>> {
    // to fetch the notes from server
    this.username = this.authService.getUserName();
    //console.log('username: ' + this.username);
    //console.log('token: ' + this.token);
    return this.http.get<Array<Tag>>(this.myAppUrl + `api/Category/GetTags/${this.username}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    });
  }

  fetchAllTagsFromServer(): Observable<Array<Tag>> {
    // to fetch the notes from server
    this.username = this.authService.getUserName();
    //console.log('username: ' + this.username);
    //console.log('token: ' + this.token);
    return this.http.get<Array<Tag>>(this.myAppUrl + `api/Category/GetAllTags`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    });
  }

  getTagByName(tagName: string): boolean {
    let record = null;
    if (this.tags.length > 0) {
      record = this.tags.find(i => i.name.toLowerCase() === tagName.toLowerCase());
    }

    //console.log('getTagByName: ' + record);
    if (record != null) {
      return true;
    }
    return false;
  }

  addTag(tag: Tag): Observable<Tag> {

    tag.createdBy=this.username;
    return this.http.post<Tag>(this.myAppUrl + `api/Category/AddTag/${this.username}`, tag, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${this.token}`)
    })
      .do(addTag => {
        this.tags.push(addTag);
        this.fetchTagsFromServer();
      })
      .catch(err => {
        return Observable.throw(err);
      });
  }

  deleteTag(tagId: Number): Observable<Tag> {
    return this.http.delete<Tag>(this.myAppUrl + `api/Category/DeleteTag/${tagId}`, {
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
          //this.fetchTagsFromServer();
        }
      })
      .catch(err => {
        return Observable.throw(err);
      });
  }
}
