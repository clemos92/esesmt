import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Menu } from '../../interfaces/menu';

@Component({
  selector: 'app-sidenav-list',
  templateUrl: './sidenav-list.component.html',
  styleUrls: ['./sidenav-list.component.scss']
})
export class SidenavListComponent implements OnInit {

  @Output() sidenavClose = new EventEmitter();
  @Input() navigation: Array<Menu> = [];
  
  constructor() { }
  
  ngOnInit() {
  }
  
  public onSidenavClose = () => {
    this.sidenavClose.emit();
  }

}
