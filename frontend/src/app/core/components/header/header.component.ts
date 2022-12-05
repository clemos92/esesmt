import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Menu } from '../../interfaces/menu';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() public sidenavToggle = new EventEmitter();
  @Input() navigation: Array<Menu> = [];
  @Input() title: string = undefined;
  @Input() logo: string = undefined;
  
  constructor(
    public router: Router
  ) { }

  ngOnInit(): void { }

  public onToggleSidenav = () => {
    this.sidenavToggle.emit();
  }

}