import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CurrencyMaskConfig, CurrencyMaskModule, CURRENCY_MASK_CONFIG } from 'ng2-currency-mask';
import { SharedModule } from '../shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ChecklistTypeListComponent } from './checklist/checklist-type-list/checklist-type-list.component';
import { ChecklistTypeRegisterComponent } from './checklist/checklist-type-register/checklist-type-register.component';
import { ModulesRoutingModule } from './modules-routing.module';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ChecklistModelListComponent } from './checklist/checklist-model-list/checklist-model-list.component';
import { ChecklistModelRegisterComponent } from './checklist/checklist-model-register/checklist-model-register.component';
import { ChecklistItemComponent } from './checklist/checklist-item/checklist-item.component';
import { ChecklistListComponent } from './checklist/checklist/checklist-list/checklist-list.component';
import { ChecklistRegisterComponent } from './checklist/checklist/checklist-register/checklist-register.component';

export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
  align: "left",
  allowNegative: false,
  decimal: ",",
  precision: 2,
  prefix: "R$ ",
  suffix: "",
  thousands: "."
};

@NgModule({
  declarations: [
    ChecklistTypeListComponent,
    ChecklistTypeRegisterComponent,
    ChecklistModelListComponent,
    ChecklistModelRegisterComponent,
    HomeComponent,
    NotFoundComponent,
    ChecklistItemComponent,
    ChecklistListComponent,
    ChecklistRegisterComponent
  ],
  imports: [
    ModulesRoutingModule,
    SharedModule,
    FlexLayoutModule,
    CurrencyMaskModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig }
  ],
  exports: [    
  ]
})
export class ModulesModule { }
