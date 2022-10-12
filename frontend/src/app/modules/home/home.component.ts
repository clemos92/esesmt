import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Menu } from 'src/app/core/interfaces/menu';
import { MenuService } from 'src/app/core/services/menu.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  shortcutMenus: Array<Menu> = [];

  constructor(private router: Router, private menuService: MenuService) { }

  ngOnInit(): void {
    this.shortcutMenus = this.menuService.getOptions();
  }
  
  navigate(route){
    this.router.navigate([route])
  }
}
