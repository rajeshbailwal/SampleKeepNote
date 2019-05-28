import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './material/material.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

import { NotesService } from './services/notes.service';
import { RouterService } from './services/router.service';
import { ToasterCustomService } from './services/toaster.service';
import { EditNoteOpenerComponent } from './note-components/edit-note-opener/edit-note-opener.component';
import { EditNoteViewComponent } from './note-components/edit-note-view/edit-note-view.component';
import { NoteComponent } from './note-components/note/note.component';
import { SnackbarComponent } from './snackbar/snackbar.component';
import { AuthenticationService } from './services/authentication.service';
import { CanActivateRouteGuard } from './can-activate-route.guard';
import { TagsComponent } from './tags/tags.component';
import { TagService } from './services/tag.service';
import { ReminderComponent } from './reminder/reminder.component';
import { ReminderService } from './services/reminder.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    DashboardComponent,
    EditNoteOpenerComponent,
    EditNoteViewComponent,
    NoteComponent,
    SnackbarComponent,
    LoginComponent,
    RegisterComponent,
    TagsComponent,
    ReminderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MaterialModule,
    ToastrModule.forRoot()
  ],
  providers: [NotesService, RouterService, TagService, ReminderService, SnackbarComponent,  ToasterCustomService, AuthenticationService,CanActivateRouteGuard],
  bootstrap: [AppComponent],
  entryComponents: [EditNoteViewComponent]
})
export class AppModule { }
