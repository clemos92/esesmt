import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChecklistTypeListComponent } from './checklist/checklist-type-list/checklist-type-list.component';
import { ChecklistModelListComponent } from './checklist/checklist-model-list/checklist-model-list.component';
import { ChecklistListComponent } from './checklist/checklist/checklist-list/checklist-list.component';

const routes: Routes = [
  {
    path: 'types', component: ChecklistTypeListComponent
  },
  {
    path: 'models', component: ChecklistModelListComponent
  },
  { 
    path: 'checklists', component: ChecklistListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ModulesRoutingModule { }
