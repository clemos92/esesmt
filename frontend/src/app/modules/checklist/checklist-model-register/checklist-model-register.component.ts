import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SnackbarServiceService } from 'src/app/core/services/snackbar-service.service';
import { SnackType } from 'src/app/shared/components/snackbar/snackbar.component';
import { ChecklistItem } from 'src/app/shared/models/checklist-item';
import { DropdownDefault } from 'src/app/shared/models/dropdown-default';
import { ChecklistTypeService } from 'src/app/shared/services/checklist-type.service';
import { ChecklistService } from 'src/app/shared/services/checklist.service';

@Component({
  selector: 'app-checklist-model-register',
  templateUrl: './checklist-model-register.component.html',
  styleUrls: ['./checklist-model-register.component.scss']
})
export class ChecklistModelRegisterComponent implements OnInit {

  form: FormGroup;
  @Output() public sendData = new EventEmitter();

  dropdownChecklistTypeDataSource: DropdownDefault[] = [];
  public items: Array<ChecklistItem> = [];

  constructor(
    private fb: FormBuilder,
    private snack: SnackbarServiceService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dataService: ChecklistService,
    private dialogRef: MatDialogRef<ChecklistModelRegisterComponent>,
    private checklistTypeService: ChecklistTypeService
  ) {
    this.form = this.fb.group({
      id: new FormControl(0),
      description: new FormControl({ value: null, disabled: this.data.action == 'details' }),
      checklistTypeId: new FormControl({ value: null, disabled: this.data.action == 'details' }),
      isActive: new FormControl({ value: '1', disabled: this.data.action == 'details' })
    });
  }

  ngOnInit(): void {
    this.getDropdowns();
    if (this.data.action == 'details' || this.data.action == 'edit') {
      this.getDetails(this.data.id);
    }
  }

  getDropdowns() {
    this.checklistTypeService.getAllActiveToDropdown().subscribe(res => {
      this.dropdownChecklistTypeDataSource = res;
    })
    this.form.get('checklistTypeId').setValue(0);
  }

  get f() { return this.form.controls; }

  public onClose(): void {
    this.dialogRef.close();
  }

  private getDetails(id) {
    this.dataService.getDetails(id).subscribe(data => {
      this.form.setValue({
        id: data.id,
        description: data.description,
        checklistTypeId: data.checklistTypeId,
        isActive: data.isActive == null ? '0' : (data.isActive ? '1' : '2')
      });
      this.items = data.checklistItems;
    });
  }

  public onSaveModel() {
    let formValue = this.form.value;
    formValue.isActive = formValue.isActive == '1' ? true : formValue.isActive == '2' ? false : null;
    formValue.checklistItems = this.items;
    if (this.data.action == 'create') {
      this.dataService.create(formValue).subscribe(data => {
        this.onSubmitCompletedWithSuccess(data);
      });
    }
    else if (this.data.action == 'edit') {
      this.dataService.edit(formValue).subscribe(data => {
        this.onSubmitCompletedWithSuccess(data);
      });
    }
  }

  public onSubmitCompletedWithSuccess(data: any) {
    this.sendData.emit(data);
    this.snack.openSnackBar('Operação realizada com sucesso.', '', SnackType.Success);
    this.onClose();
  }

  getChecklistItemByName(name: string, checklistItem: ChecklistItem) {
    return checklistItem.name == name;
  }

  public updateItems(data: any) {
    if (data.nextValue.id == 0) {
      if (data.previousValue == null) {
        this.items.push(data.nextValue);
      }
      else {
        var changedItems = this.items.filter(x => this.getChecklistItemByName(data.previousValue.name, x));
        if (changedItems.length != 0) {
          changedItems[0].name = data.nextValue.name;
          changedItems[0].isActive = data.nextValue.isActive;
        }
      }
    }
    else {
      this.items.filter((value, key) => {
        if (value.id == data.nextValue.id) {
          value.name = data.nextValue.name;
          value.isActive = data.nextValue.isActive;
        }
        return true;
      });
    }
  }
}