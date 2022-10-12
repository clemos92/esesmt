import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Menu } from '../../interfaces/menu';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Input() navigation: Array<Menu> = [];
  @Input() title: string = undefined;
  @Input() logo: string = undefined;
  
  constructor(
    public router: Router
  ) { }

  ngOnInit(): void { }

}