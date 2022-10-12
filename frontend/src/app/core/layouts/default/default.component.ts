import { Component, OnInit } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { ProgressSpinnerMode } from '@angular/material/progress-spinner';
import { delay } from 'rxjs/operators';
import { Menu } from '../../interfaces/menu';
import { LoadingService } from '../../services/loading.service';
import { MenuService } from '../../services/menu.service';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.scss']
})
export class DefaultComponent implements OnInit {

  navigation: Array<Menu> = [] 
  title = 'Sistema de Gestão e Controle do SESMT';

  color: ThemePalette = 'primary';
  mode: ProgressSpinnerMode = 'indeterminate';
  value = 50;

  loading: boolean = false;

  constructor(public loadingService: LoadingService, private menuService: MenuService) {}

  ngOnInit(): void {
    this.listenToLoading();
    this.navigation = this.menuService.getOptions();
  }

  listenToLoading(): void {
    this.loadingService.loadingSub
      .pipe(delay(0)) // This prevents a ExpressionChangedAfterItHasBeenCheckedError for subsequent requests
      .subscribe((loading) => {
        this.loading = loading;
      });
  }
}
