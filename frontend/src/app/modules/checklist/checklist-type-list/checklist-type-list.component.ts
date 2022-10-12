import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ChecklistType } from 'src/app/shared/models/checklist-type';
import { ChecklistTypeService } from 'src/app/shared/services/checklist-type.service';
import { ChecklistTypeRegisterComponent } from '../checklist-type-register/checklist-type-register.component';

@Component({
  selector: 'app-checklist-type-list',
  templateUrl: './checklist-type-list.component.html',
  styleUrls: ['./checklist-type-list.component.scss']
})
export class ChecklistTypeListComponent implements OnInit {

  form: FormGroup;

  dataSource = new MatTableDataSource<ChecklistType>();
  displayedColumns: string[] = [
    'actions',
    'name',
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
    private dataService: ChecklistTypeService,
    private dialog: MatDialog 
  ) {
    this.form = this.fb.group({
      name: new FormControl(null),      
      isActive: new FormControl(null)
    });
  }

  ngOnInit(): void {
    this.form.get('isActive').setValue(0);
    this.loadData(null);    
  }

  onSubmit(){
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
      name: this.f.name.value,
      isActive: this.f.isActive.value == 0 ? null : (this.f.isActive.value == '1' ? true : false),
      sortDirection: this.sort?.direction,
      pageIndex: this.paginator?.pageIndex,
      pageSize: this.paginator?.pageSize,
      sortBy: this.sort?.active ?? ''
    }

    return filter;
  }

  openDialog(action: string, identifier: number = 0, refreshList: boolean = true){
    let dialogRef = this.dialog.open(ChecklistTypeRegisterComponent, {
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
