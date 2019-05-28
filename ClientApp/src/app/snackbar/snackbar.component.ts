import { Component, OnInit } from '@angular/core';
import { MatSnackBarConfig, MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material';

@Component({
  selector: 'app-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.css']
})
export class SnackbarComponent implements OnInit {

  actionButtonLabel: string = 'Close';
  action: boolean = true;
  setAutoHide: boolean = true;
  autoHide: number = 2000;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  addExtraClass: boolean = false;

  config = new MatSnackBarConfig();

constructor(public snackBar: MatSnackBar) { }

ngOnInit() {
  this.config.verticalPosition = 'top';
  this.config.horizontalPosition = this.horizontalPosition;
  this.config.duration = 2000;
  this.config.panelClass = ['test'];
}

  public snackbarSucces(message) {
    this.config.verticalPosition = 'top';
    this.config.duration = 2000;
    this.config.panelClass = ['test'];
  this.snackBar.open(message, this.action ? this.actionButtonLabel : undefined, this.config);
}

  private configSucces: MatSnackBarConfig = {
  panelClass: ['test'],
  duration: 1000,
  verticalPosition: 'top'
};

  private configError: MatSnackBarConfig = {
  panelClass: ['style-error'],
};

  //public snackbarSucces(message) {
  //  this.snackBar.open(message, 'close', this.configSucces);
  //}

  public snackbarError(message) {
  this.snackBar.open(message, 'close', this.configError);
}
}
