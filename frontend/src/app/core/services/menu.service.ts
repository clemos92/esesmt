import { Menu } from '../interfaces/menu';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MenuService {

  //icones -> https://www.angularjswiki.com/angular/angular-material-icons-list-mat-icon-list/
  getOptions(): Array<Menu> {
    return [
      { link: '/types', label: 'Tipos', icon: "settings" },
      { link: '/models', label: 'Modelos', icon: "list_alt" },
      { link: '/checklists', label: 'Checklist', icon: "checklist_rtl" }
    ];
  }
}