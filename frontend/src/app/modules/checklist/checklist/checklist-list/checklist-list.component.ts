import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CompletedChecklist } from 'src/app/shared/models/completed-checklist';
import { DropdownDefault } from 'src/app/shared/models/dropdown-default';
import { ChecklistTypeService } from 'src/app/shared/services/checklist-type.service';
import { CompletedChecklistService } from 'src/app/shared/services/completed-checklist.service';
import { ChecklistRegisterComponent } from '../checklist-register/checklist-register.component';

@Component({
  selector: 'app-checklist-list',
  templateUrl: './checklist-list.component.html',
  styleUrls: ['./checklist-list.component.scss']
})
export class ChecklistListComponent implements OnInit {

  form: FormGroup;
  dropdownChecklistTypeDataSource: DropdownDefault[] = [];
  
  dataSource = new MatTableDataSource<CompletedChecklist>();
  displayedColumns: string[] = [
    'actions',
    'checklistDescription',
    'checklistChecklistTypeName',
    'creationDate'
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  pageEvent: PageEvent;
  pageIndex: number = 0;
  pageSize: number = 10;
  length: number;

  constructor(
    private fb: FormBuilder,
    private dataService: CompletedChecklistService,
    private checklistTypeService: ChecklistTypeService,
    private dialog: MatDialog 
  ) {
    this.form = this.fb.group({
      description: new FormControl(null),
      checklistTypeId: new FormControl(null),      
      startDate: new FormControl(null),
      endDate: new FormControl(null)
    });
  }

  ngOnInit(): void {
    this.getDropdowns();
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
      startDate: this.f.startDate.value,
      endDate: this.f.endDate.value,
      sortDirection: this.sort?.direction,
      pageIndex: this.paginator?.pageIndex ?? this.pageIndex,
      pageSize: this.paginator?.pageSize ?? this.pageSize,
      sortBy: this.sort?.active ?? ''
    }

    return filter;
  }

  openDialog(action: string, identifier: number = 0, refreshList: boolean = true){
    let dialogRef = this.dialog.open(ChecklistRegisterComponent, {
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
