import { AfterViewChecked, ChangeDetectorRef, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ChecklistItem } from 'src/app/shared/models/checklist-item';

@Component({
  selector: 'app-checklist-item',
  templateUrl: './checklist-item.component.html',
  styleUrls: ['./checklist-item.component.scss']
})
export class ChecklistItemComponent implements AfterViewChecked {

  form: FormGroup;

  @Input() public checklistItems: Array<ChecklistItem> = [];
  @Input() public checklistIdentifier: number = 0;
  @Input() public action: string;

  private previous: ChecklistItem;
  public isDisabled: boolean = false;

  @Output() public sendData = new EventEmitter();

  dataSource = new MatTableDataSource<ChecklistItem>();
  displayedColumns: string[] = [
    'actions',
    'name',
    'isActive'
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private fb: FormBuilder, private cdRef: ChangeDetectorRef) {
    this.form = this.fb.group({
      id: new FormControl(0),
      name: [null, Validators.required],
      checklistId: new FormControl(this.checklistIdentifier),
      isActive: [true, Validators.required]
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngAfterViewChecked() {
    if (this.action == 'details') {
      this.form.controls.name.disable();
      this.form.controls.isActive.disable();
      this.isDisabled = true;
    }
    this.form.patchValue({ checklistId: this.checklistIdentifier });
    this.loadData();
    this.cdRef.detectChanges();
  }

  onSaveItem() {
    this.sendData.emit({ nextValue: this.form.value, previousValue: this.previous });
    this.onClear();
  }

  get f() { return this.form.controls; }

  onEdit(dataRow) {
    if (!this.isDisabled) {
      this.previous = dataRow;
      this.form.setValue({
        id: dataRow.id,
        name: dataRow.name,
        checklistId: dataRow.checklistId,
        isActive: dataRow.isActive
      });
    }
  }

  public onClear() {
    this.previous = null;
    this.form.setValue({
      id: 0,
      name: null,
      checklistId: this.checklistIdentifier,
      isActive: true
    });
  }

  public loadData() {
    this.dataSource = new MatTableDataSource<any>(this.checklistItems);
  }

}
