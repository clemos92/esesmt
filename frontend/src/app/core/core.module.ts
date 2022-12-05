import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { DefaultComponent } from './layouts/default/default.component';
import { SharedModule } from '../shared/shared.module';
import { CoreRoutingModule } from './core-routing.module';
import { CachingInterceptor } from './cache/caching-interceptor.service';
import { SidenavListComponent } from './components/sidenav-list/sidenav-list.component';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    DefaultComponent,
    SidenavListComponent
  ],
  imports: [
    CoreRoutingModule,
    FlexLayoutModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    // será usado a estratégia de cache do Service Work 
    // { provide: HTTP_INTERCEPTORS, useClass: CachingInterceptor, multi: true }
  ],
  exports: [    
    CoreRoutingModule,
    HeaderComponent,
    FooterComponent,
    DefaultComponent
  ]
})
export class CoreModule { }
