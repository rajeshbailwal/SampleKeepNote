import { NgModule } from '@angular/core';

import {
  MatInputModule, MatFormFieldModule, MatToolbarModule,
  MatExpansionModule, MatCardModule, MatButtonModule,
  MatCheckboxModule, MatSidenavModule, MatIconModule,
  MatListModule, MatDialogModule, MatOptionModule, MatSelectModule,
  MatChipsModule, MatSnackBarModule, MatAutocompleteModule
} from '@angular/material';

@NgModule({

  imports: [
    MatInputModule, MatFormFieldModule, MatToolbarModule,
    MatExpansionModule, MatCardModule, MatButtonModule,
    MatCheckboxModule, MatSidenavModule, MatIconModule,
    MatListModule, MatDialogModule, MatOptionModule, MatSelectModule,
    MatChipsModule, MatSnackBarModule, MatAutocompleteModule
  ],
  exports: [
    MatInputModule, MatFormFieldModule, MatToolbarModule,
    MatExpansionModule, MatCardModule, MatButtonModule,
    MatCheckboxModule, MatSidenavModule, MatIconModule,
    MatListModule, MatDialogModule, MatOptionModule, MatSelectModule,
    MatChipsModule, MatSnackBarModule, MatAutocompleteModule
  ]
})

export class MaterialModule { }
