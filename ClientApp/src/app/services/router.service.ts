import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Injectable()
export class RouterService {

  constructor(private _router: Router, private _location: Location) { }

  routeToDashboard() {
    this._router.navigate(['dashboard']);
  }

  routeToLogin() {
    this._router.navigate(['login']);
  }

  routeToEditNoteView(noteId) {
    //console.log('noteid:' + noteId)
    //this._router.navigate(['dashboard/edit/' + noteId]);
    //this._router.navigate([
    //  'dashboard', {
    //    outlets: {
    //      noteEditOutlet: ['note', noteId, 'edit']
    //    }
    //  }
    //]);

    this._router.navigate([
      'dashboard', 'note', noteId, 'edit'
    ]);
  }

  routeBack() {
    this._location.back();
  }

  routeToRegister(){
    this._router.navigate(['register']);
  }
}
