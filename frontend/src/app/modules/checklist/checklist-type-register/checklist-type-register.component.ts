import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

//Services
import { ChecklistTypeService } from 'src/app/shared/services/checklist-type.service';
import { SnackbarServiceService } from 'src/app/core/services/snackbar-service.service';

//Models
import { SnackType } from 'src/app/shared/components/snackbar/snackbar.component';

@Component({
  selector: 'app-checklist-type-register',
  templateUrl: './checklist-type-register.component.html',
  styleUrls: ['./checklist-type-register.component.scss']
})
export class ChecklistTypeRegisterComponent implements OnInit {

  form: FormGroup;

  @Output() public sendData = new EventEmitter();
  
  constructor(
    private fb: FormBuilder,
    private snack: SnackbarServiceService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dataService: ChecklistTypeService,
    private dialogRef: MatDialogRef<ChecklistTypeRegisterComponent>,
  ) {
    this.form = this.fb.group({
      id: new FormControl(0),
      name: new FormControl({ value: null, disabled: this.data.action == 'details' }),
      isActive: new FormControl({ value: '1', disabled: this.data.action == 'details' })
    });
  }

  ngOnInit(): void {
    if (this.data.action == 'details' || this.data.action == 'edit') {
      this.getDetails(this.data.id);
    }
  }

  get f() { return this.form.controls; }

  public onClose(): void {
    this.dialogRef.close();
  }
		
  private getDetails(id) {
    this.dataService.getDetails(id).subscribe(data => {
      this.form.setValue({
        id: data.id,
        name: data.name,
        isActive: data.isActive == null ? '0' : (data.isActive ? '1' : '2')
      });
    });
  }

  public onSave() {
    let formValue = this.form.value;
    formValue.isActive = formValue.isActive == '1' ? true : formValue.isActive == '2' ? false : null;
    
    if(this.data.action == 'create'){
      this.dataService.create(formValue).subscribe(data=>{
        this.onSubmitCompletedWithSuccess(data);
      });
    }
    else if(this.data.action == 'edit'){
      this.dataService.edit(formValue).subscribe(data=>{
        this.onSubmitCompletedWithSuccess(data);
      });
    }
  }

  public onSubmitCompletedWithSuccess(data: any){
    this.sendData.emit(data);
    this.snack.openSnackBar('Operação realizada com sucesso.', '', SnackType.Success);
    this.onClose();
  }
}
