import { ChangeDetectorRef, Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SnackbarServiceService } from 'src/app/core/services/snackbar-service.service';
import { SnackType } from 'src/app/shared/components/snackbar/snackbar.component';
import { CompletedChecklistItem } from 'src/app/shared/models/completed-checklist-item';
import { DropdownDefault } from 'src/app/shared/models/dropdown-default';
import { ChecklistService } from 'src/app/shared/services/checklist.service';
import { CompletedChecklistService } from 'src/app/shared/services/completed-checklist';


@Component({
  selector: 'app-checklist-register',
  templateUrl: './checklist-register.component.html',
  styleUrls: ['./checklist-register.component.scss']
})
export class ChecklistRegisterComponent implements OnInit {

  @Output() public sendData = new EventEmitter();

  form: FormGroup;
  dropdownChecklistDataSource: DropdownDefault[] = [];

  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<ChecklistRegisterComponent>,
    private dataService: CompletedChecklistService,
    private cdRef: ChangeDetectorRef,
    private snack: SnackbarServiceService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private checklistService: ChecklistService) {
    this.form = this.fb.group({
      id: new FormControl(0),
      creationDate: new FormControl({ value: null, disabled: this.data.action == 'details' }),
      checklistId: new FormControl({ value: 0, disabled: this.data.action == 'details' }),
      completedChecklistItems: this.fb.array([])
    });
  }

  ngOnInit() {
    if (this.data.action == 'details') {
      this.getDetails(this.data.id);
    }
    else if (this.data.action == 'create') {
      this.getDropdowns();
    }
  }

  private getDetails(id) {
    this.dataService.getDetails(id).subscribe(data => {
      this.getDropdowns(data.checklistId);

      this.form.patchValue({
        id: data.id,
        checklistId: data.checklistId,
        creationDate: data.creationDate
      });

      for (const item of data.completedChecklistItems) {
        this.addFormField(item);
      }
    });
  }

  onSelectChecklist(checklistId: number) {
    this.formArrayItems.clear();

    if (checklistId != 0) {
      this.checklistService.getDetails(checklistId).subscribe((checklist) => {
        this.form.patchValue({
          checklistId: checklist.id
        });

        for (const checklistItem of checklist.checklistItems) {
          let completedChecklistItem = new CompletedChecklistItem();
          completedChecklistItem.id = 0;
          completedChecklistItem.completedChecklistId = 0;
          completedChecklistItem.checklistItemId = checklistItem.id;
          completedChecklistItem.checklistItemName = checklistItem.name;
          completedChecklistItem.observation = null;
          completedChecklistItem.status = true;

          this.addFormField(completedChecklistItem);
        }
      });
    }
  }

  getDropdowns(id: number = 0) {
    if (id != 0) {
      this.checklistService.getByIdToDropdown(id).subscribe(res => {
        this.dropdownChecklistDataSource = res;
      })
    }
    else {
      this.checklistService.getAllActiveToDropdown().subscribe(res => {
        this.dropdownChecklistDataSource = res;
      })
    }
    this.form.get('checklistId').setValue(0);
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
    return this.form.get('completedChecklistItems') as FormArray;
  }

  addFormField(completedChecklistItem: CompletedChecklistItem) {
    let formGroup = this.fb.group({
      id: new FormControl({ value: completedChecklistItem.id, disabled: this.data.action == 'details' }),
      completedChecklistItemId: new FormControl({ value: completedChecklistItem.completedChecklistId, disabled: this.data.action == 'details' }),
      checklistItemId: new FormControl({ value: completedChecklistItem.checklistItemId, disabled: this.data.action == 'details' }),
      title: new FormControl({ value: completedChecklistItem.checklistItemName, disabled: this.data.action == 'details' }),
      observation: new FormControl({ value: completedChecklistItem.observation, disabled: this.data.action == 'details' }),
      status: new FormControl({ value: completedChecklistItem.status, disabled: this.data.action == 'details' })
    });
    this.formArrayItems.push(formGroup);

    this.configObservationValidator(completedChecklistItem.status, formGroup);
  }

  configObservationValidator(eventValue: boolean, item: FormGroup) {
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

  public onSave() {
    console.log(this.form.value);
    if(this.data.action == 'create'){
      this.dataService.create(this.form.value).subscribe(data=>{
        this.onSubmitCompletedWithSuccess(data);
      });
    }
  }
}
