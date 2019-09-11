import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyComponent } from './company/company.component';
import { AddCompanyComponent } from './company/add-company/add-company.component';
import { MasterRoutingModule } from './master.routing';
import { ProductMasterComponent } from './product-master/product-master.component';
import { AddProductMasterComponent } from './product-master/add-product-master/add-product-master.component'
import { ImportModule } from '../../import.module';

@NgModule({
  declarations: [CompanyComponent, AddCompanyComponent, ProductMasterComponent, AddProductMasterComponent],
  imports: [
    CommonModule,
    MasterRoutingModule, ImportModule
  ]
})
export class MastersModule { }
