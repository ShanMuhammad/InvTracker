import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { AuthGuard } from './Core/Guard/Auth.gurad';
import { APP_BASE_HREF } from '@angular/common';

import { LayoutComponent } from './modules/layout/layout/layout.component'
import { LoginComponent } from './component/login/login.component';





export const routes: Routes = [


  //{
  //  path: '',
  //  component: LoginComponent
  //},
  //{
  //  path: 'ref/:Path',
  //  component: LoginComponent
  //},
  {
    path: '', component: LayoutComponent,
    children: [
      {
        path: 'Master', loadChildren: () => import('./modules/masters/masters.module').then(m => m.MastersModule) 
      },
    ]
  },

  //{
  //  path: 'logout', component: LogoutComponent
  //},
  //{
  //  path: 'errorHandler', component: ErrorLogComponent
  //},
  //{
  //  path: 'sessionexpired', component: SessionExpiredComponent
  //},
  //{
  //  path: '**',
  //  component: NotFoundComponent
  //}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [{ provide: APP_BASE_HREF, useValue: '/' }]
  //providers: [{ provide: APP_BASE_HREF, useValue: '/' }, AuthGuard]
})
export class AppRoutingModule { }
