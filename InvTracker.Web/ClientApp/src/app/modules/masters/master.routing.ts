import { Routes, RouterModule } from '@angular/router';
import { CompanyComponent } from './company/company.component';
import { AddCompanyComponent } from './company/add-company/add-company.component';
import { NgModule } from '@angular/core';
import { ProductMasterComponent } from './product-master/product-master.component';
import { AddProductMasterComponent } from './product-master/add-product-master/add-product-master.component'


const routes: Routes = [
  { path: 'company', component: CompanyComponent },
  { path: 'company/add', component: AddCompanyComponent },
  { path: 'product', component: ProductMasterComponent },
  { path: 'product/add', component: AddProductMasterComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
