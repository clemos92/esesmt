import { stringify } from '@angular/compiler/src/util';
import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';

@Component({
  selector: 'app-generic-mat-table',
  templateUrl: './generic-mat-table.component.html',
  styleUrls: ['./generic-mat-table.component.scss']
})
export class GenericMatTableComponent {

  @Output("onAction") emitter = new EventEmitter();
  @Output("onPageChanged") pageChangedEmitter = new EventEmitter();
  @Output("onSortData") sortDataEmitter = new EventEmitter();
  @Output() onCellBinding = new EventEmitter();
  
  @ViewChild(MatPaginator, {static:false}) matPaginator: MatPaginator;
  @ViewChild(MatSort) matSort: MatSort;

  @Input("data") dataSource = [];
  @Input("columns") tableCols = [];
  @Input("length") length: number;
  @Input("caption") caption: string;
  @Input("noDataColspan") noDataColspan: number;
    
  cellMaxLength = 50;
  pageIndex: number = 0;
  pageSize: number = 10;

  get keys() {
    return this.tableCols.filter(opt => !opt.columnSettings?.hidden).map(opt => opt.key);
  }

  class(elt, col){
    let className='';
    this.onCellBinding.emit({
      data: { elt, col }, 
      func: (data_from_parent => {
        className = data_from_parent
      })
    });
    return className;
  }

  showBooleanValue(elt, column) {
    return column.config.values[`${elt[column.key]}`];
  }

  onPageChanged(pageEvent: PageEvent) {
    this.pageChangedEmitter.emit({ pageParameters: this.matPaginator, sortParameters: this.matSort });
  }

  onSortData(sortParameters: Sort) {
    this.sortDataEmitter.emit({ pageParameters: this.matPaginator, sortParameters: this.matSort });
  }

  onClick(dataRow, action){
    this.emitter.emit({action, dataRow});
  }

  resetConfig() {
    this.matPaginator.pageIndex = 0;
    this.matPaginator.pageSize = 10;
    this.matSort.sort({ id: null, start: 'asc', disableClear: false });
  }

}
