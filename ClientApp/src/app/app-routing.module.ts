import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditNoteOpenerComponent } from './note-components/edit-note-opener/edit-note-opener.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CanActivateRouteGuard } from './can-activate-route.guard';
import { TagsComponent } from './tags/tags.component';
import { ReminderComponent } from './reminder/reminder.component';

const routes: Routes = [
  
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  //{ path: 'dashboard/edit/:noteId', component: EditNoteOpenerComponent },
  
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [ CanActivateRouteGuard ],
    children: [
      {
        path: 'note/:noteId/edit',
        component: EditNoteOpenerComponent,
        //outlet: 'noteEditOutlet'
      }
    ]
  },
  { path: '', component: DashboardComponent, canActivate: [ CanActivateRouteGuard ], pathMatch: 'full' },
  {
    path: 'tags',
    component: TagsComponent,
    canActivate: [ CanActivateRouteGuard ]
  },
  {
    path: 'reminders',
    component: ReminderComponent,
    canActivate: [ CanActivateRouteGuard ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
