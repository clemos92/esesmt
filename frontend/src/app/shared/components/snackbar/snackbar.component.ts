import { Component, Inject, OnInit } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';

export enum SnackType {
  Success,
  Error,
  Warn,
  Info,
}

@Component({
  selector: 'app-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.scss']
})
export class SnackbarComponent implements OnInit {

  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) {
    console.log(data);
  }

  ngOnInit(): void {
  }

  get getIcon() {
    switch (this.data.snackType) {
      case SnackType.Success:
        return 'done';
      case SnackType.Error:
        return 'error';
      case SnackType.Warn:
        return 'warning';
      case SnackType.Info:
        return 'info';
    }
  }

}
