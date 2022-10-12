import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarComponent, SnackType } from 'src/app/shared/components/snackbar/snackbar.component';

@Injectable({
  providedIn: 'root'
})
export class SnackbarServiceService {
  
  constructor(private snackBar: MatSnackBar) {}

  public openSnackBar(message: string, action: string, snackType?: SnackType) {
    const _snackType: SnackType =
      snackType !== undefined ? snackType : SnackType.Success;

    this.snackBar.openFromComponent(SnackbarComponent, {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      data: { message: message, snackType: _snackType }
    });
  }

}
