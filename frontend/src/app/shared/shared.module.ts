import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { NgxMatFileInputModule } from '@angular-material-components/file-input';
import { SnackbarComponent } from './components/snackbar/snackbar.component';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { GenericMatTableComponent } from './components/generic-mat-table/generic-mat-table.component';
import { DialogComponent } from './components/dialog/dialog.component';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';

@NgModule({
  declarations: [
    SnackbarComponent,
    ConfirmDialogComponent,
    FileUploadComponent,
    GenericMatTableComponent,
    DialogComponent,
    SafeHtmlPipe
  ],
  entryComponents: [
    ConfirmDialogComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FlexLayoutModule,
    NgxMatFileInputModule
  ],
  exports: [    
    CommonModule,
    MaterialModule,
    NgxMatFileInputModule,
    SnackbarComponent,
    ConfirmDialogComponent,
    FileUploadComponent,
    GenericMatTableComponent,
    DialogComponent,
    SafeHtmlPipe
  ]
})
export class SharedModule { }
