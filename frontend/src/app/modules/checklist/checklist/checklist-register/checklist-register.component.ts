import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, FormArray, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SnackbarServiceService } from 'src/app/core/services/snackbar-service.service';
import { SnackType } from 'src/app/shared/components/snackbar/snackbar.component';
import { Checklist } from 'src/app/shared/models/checklist';
import { FormField } from 'src/app/shared/models/form-field';


@Component({
  selector: 'app-checklist-register',
  templateUrl: './checklist-register.component.html',
  styleUrls: ['./checklist-register.component.scss']
})
export class ChecklistRegisterComponent implements OnInit {

  @Output() public sendData = new EventEmitter();
  
  public checklistModel: Checklist = {
    id: 4,
    description: 'Luna',
    checklistTypeId: 4,
    checklistTypeName: 'Carlos Lemos',
    checklistItems: [{
      id: 2,
      checklistId: 4,
      name: 'dfdf 555 dddd',
      isActive: true
    },
    {
      id: 3,
      checklistId: 4,
      name: 'dfdafd',
      isActive: true
    },
    {
      id: 4,
      checklistId: 4,
      name: 'dafdfdsa ss',
      isActive: true
    }]
  };

  form: FormGroup;

  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<ChecklistRegisterComponent>,
    private httpClient: HttpClient, 
    private cdRef:ChangeDetectorRef,
    private snack: SnackbarServiceService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.form = new FormGroup({
      id: new FormControl(4),
      checklistItems: this.fb.array([])
    });
  }

  ngOnInit() {
    this.httpClient.get<FormField[]>("/assets/form.json").subscribe((formFields) => {
      for (const formField of formFields) {
        this.addFormField(formField);
      }
    });
  }

  ngAfterViewChecked() {
    this.cdRef.detectChanges();
  }

  onSubmit(): void {
    if (this.form.valid) {
      let value = this.form.value;
    }
  }

  public onClose(): void {
    this.dialogRef.close();
  }

  get formArrayItems() {
    return this.form.get('checklistItems') as FormArray;
  }

  addFormField(formField: FormField) {
    const checklistItems = this.formArrayItems;
    let formGroup = this.fb.group({
      id: [formField.id],
      title: [formField.title],
      observation: [formField.observation],
      status: [formField.status]
    });
    checklistItems.push(formGroup);

    this.configObservationValidator(formField.status, formGroup);
  }

  configObservationValidator(eventValue: boolean, item: FormGroup){
    if (eventValue) {
      item.controls.observation.setValue(null);
      item.controls.observation.clearValidators();
    }
    else {
      item.controls.observation.setValidators(Validators.required);
    }
    item.controls.observation.updateValueAndValidity();
  }

  public onSubmitCompletedWithSuccess(data: any) {
    this.sendData.emit(data);
    this.snack.openSnackBar('Operação realizada com sucesso.', '', SnackType.Success);
    this.onClose();
  }
}
