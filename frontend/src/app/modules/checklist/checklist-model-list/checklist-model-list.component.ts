import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Checklist } from 'src/app/shared/models/checklist';
import { DropdownDefault } from 'src/app/shared/models/dropdown-default';
import { ChecklistTypeService } from 'src/app/shared/services/checklist-type.service';
import { ChecklistService } from 'src/app/shared/services/checklist.service';
import { ChecklistModelRegisterComponent } from '../checklist-model-register/checklist-model-register.component';

@Component({
  selector: 'app-checklist-model-list',
  templateUrl: './checklist-model-list.component.html',
  styleUrls: ['./checklist-model-list.component.scss']
})
export class ChecklistModelListComponent implements OnInit {

  form: FormGroup;
  dropdownChecklistTypeDataSource: DropdownDefault[] = [];
  
  dataSource = new MatTableDataSource<Checklist>();
  displayedColumns: string[] = [
    'actions',
    'description',
    'checklistTypeName',
    'isActive'
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  pageEvent: PageEvent;
  pageIndex: number = 0;
  pageSize: number = 10;
  length: number;

  constructor(
    private fb: FormBuilder,
    private dataService: ChecklistService,
    private checklistTypeService: ChecklistTypeService,
    private dialog: MatDialog 
  ) {
    this.form = this.fb.group({
      description: new FormControl(null),
      checklistTypeId: new FormControl(null),      
      isActive: new FormControl(null)
    });
  }

  ngOnInit(): void {
    this.getDropdowns();
    this.form.get('isActive').setValue(0);
    this.loadData(null);    
  }

  getDropdowns() {
    this.checklistTypeService.getAllActiveToDropdown().subscribe(res => {
      this.dropdownChecklistTypeDataSource = res;
    })  
    this.form.get('checklistTypeId').setValue(0);
  }

  onSearch(){
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = 10;
    this.sort.sort({ id: null, start: 'asc', disableClear: false });
    this.loadData(null);
  }

  get f() { return this.form.controls; }

  public onSortData(sort: Sort) {
    this.loadData(null);
  }

  onViewDetails(dataRow) {
    this.openDialog('details', dataRow.id, false);
  }

  onEdit(dataRow) {
    this.openDialog('edit', dataRow.id);
  }
  
  public add(){
    this.openDialog('create');
  }

  public loadData(event?: PageEvent) {
     let filter = this.getFilter();

    this.dataService.getPagedByFilter(filter).subscribe(response => {
      this.dataSource = new MatTableDataSource<any>(response.data);
      this.pageIndex = response.pageIndex;
      this.pageSize = response.pageSize;
      this.length = response.totalRecords;
    });

    return event;
  }

  private getFilter() {
    let filter = {
      description: this.f.description.value,
      checklistTypeId: this.f.checklistTypeId.value == 0 ? null : this.f.checklistTypeId.value,
      isActive: this.f.isActive.value == 0 ? null : (this.f.isActive.value == '1' ? true : false),
      sortDirection: this.sort?.direction,
      pageIndex: this.paginator?.pageIndex,
      pageSize: this.paginator?.pageSize,
      sortBy: this.sort?.active ?? ''
    }

    return filter;
  }

  openDialog(action: string, identifier: number = 0, refreshList: boolean = true){
    let dialogRef = this.dialog.open(ChecklistModelRegisterComponent, {
      width: '840px',
      data: { 
        id: identifier,
        action: action 
      }
    });

    if (refreshList) {
      dialogRef.componentInstance.sendData.subscribe(result => {
        this.loadData(null);
      });
    }
  }

}