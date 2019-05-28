import { Component, OnInit } from '@angular/core';
import { ToasterCustomService } from '../services/toaster.service';
import { ReminderService } from '../services/reminder.service';
import { Reminder } from '../models/reminder';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-reminder',
  templateUrl: './reminder.component.html',
  styleUrls: ['./reminder.component.css']
})
export class ReminderComponent implements OnInit {

  remind: Reminder = new Reminder();
  reminders: Array<Reminder> = [];

  constructor(private reminderService: ReminderService, private toasterService: ToasterCustomService) { }

  ngOnInit() {
    this.getReminders();
  }

  getReminders() {

    this.reminderService.fetchRemindersFromServer().subscribe(data => {

      this.reminders = data;
      //console.log(this.reminders);
    });

    //console.log(this.reminders);
  }

  addReminder(form: NgForm) {
    const formData = this.remind;
    event.preventDefault();

    //console.log(formData);

    if (!formData.name || formData.name.length === 0 || !formData.description || formData.description.length === 0) {
      this.toasterService.showError('Name and Description both are required fields');
    } else {

      var result = this.reminderService.getReminderByName(this.remind.name);

      if (result) {
        this.toasterService.showError("Reminder is already exist with same name.");
      }
      else {
        //this.notes.push(this.note);
        this.reminderService.addReminder(this.remind).subscribe(
          data => {
            this.toasterService.showSuccess('Reminder added successfully.');

            //console.log('addReminder: ' + data);
            this.remind = new Reminder();
            form.reset();
            this.getReminders();
          },
          error => {
            if (error.status === 404) {
              this.toasterService.showError(error.error==null ? error.message : error.error);
            } else {
              this.toasterService.showError(error.error==null ? error.message : error.error);
            }
          }
        );

      }
    }
  }

  deleteReminder(event, reminderId) {
    //console.log(reminderId);
    this.reminderService.deleteReminder(reminderId).subscribe(
      data => {
        //this.getNotes();
        this.toasterService.showSuccess('Reminder Deleted Successfully.');
        this.getReminders();
       },
      error => {
        //console.log(JSON.stringify(error));
        this.toasterService.showError(error.error==null ? error.message : error.error);
      }
    );
  }
}
