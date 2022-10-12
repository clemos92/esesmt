import { Menu } from '../interfaces/menu';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
    
    getOptions(): Array<Menu> {
        return [
            { link: '/types', label: 'Tipos', icon: "pending_actions" },
            { link: '/models', label: 'Modelos', icon:"history" },
            { link: '/checklists', label: 'Checklist', icon:"forward_to_inbox" },
            { link: '/axis', label: 'Eixos', icon:"track_changes" },
          ];
    }
}