import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';

//Import Component Section
import { LayoutComponent } from './modules/layout/layout/layout.component'
import { HeaderComponent } from './modules/layout/header/header.component';
import { FooterComponent } from './modules/layout/footer/footer.component';
import { LoginComponent } from './component/login/login.component';
import { AppTokenHandler } from './common/AppTokenHandler';
// End Component Section

//Import Service Section
import { DataService } from './core/data.service'
import { CommonService } from './shared/common.service'
// END Service Section

//Import Modules

import { ImportModule } from './import.module';
// END



import { routes } from './app.routing.module';





@NgModule({
  declarations: [
    AppComponent, LayoutComponent, HeaderComponent, FooterComponent, LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule, BrowserAnimationsModule,
    FormsModule,
    ImportModule,
    RouterModule.forRoot(routes)
  ],
  providers: [DataService, CommonService, AppTokenHandler],
  bootstrap: [AppComponent]
})
export class AppModule { }
