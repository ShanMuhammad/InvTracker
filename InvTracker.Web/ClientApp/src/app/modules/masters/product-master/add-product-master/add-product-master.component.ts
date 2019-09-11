import { Component, OnInit } from '@angular/core';
import { ProductMasterService } from '../../../../shared/product-master.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../../../shared/common.service';





@Component({
  selector: 'app-add-product-master',
  templateUrl: './add-product-master.component.html',
  styleUrls: ['./add-product-master.component.css']
})
export class AddProductMasterComponent implements OnInit {

  AddProductForm: FormGroup;
  ProductCategoryList: any;
  ProductSubCategoryList: any;
  ProductCompanyList: any;
  UnitList: any;
  TaxList: any;

  constructor(private _productService: ProductMasterService, private fb: FormBuilder, private _commonService: CommonService) {

    this.AddProductForm = this.fb.group({
      ProductCategoryId: [null, [Validators.required]],
      ProductSubCategoryId: [],
      ProductCode: [null, [Validators.required]],
      ProductName: [],
      ProductDescription: [],
      ProductCompanyId: [],
      Packing: [],
      PurchaseUnitId: [],
      SalesUnitId: [],
      PuchaseTaxId: [],
      SaleTaxId: [],
      TaxAplicability: [],
      HSNCode: [],
      SACCode: [],
      IsActive: ['true'],

    });


  }

  ngOnInit() {
    this._commonService.BindDropDown('ProdCat').subscribe(data => {
      this.ProductCategoryList = data;
    });
    this._commonService.BindDropDown('ProdComp').subscribe(data => {
      this.ProductCompanyList = data;
    });
    this._commonService.BindDropDown('TaxMaster').subscribe(data => {
      this.TaxList = data;
    });
    this._commonService.BindDropDown('UnitMaster').subscribe(data => {
      this.UnitList = data;
    });

  }
  getProductSubCategory(ProductCategoryId) {
    this._commonService.BindDropDown('ProdSubCat', ProductCategoryId).subscribe(data => {
      this.ProductSubCategoryList = data;
    });
  }
  getFormControls(controlName): any {
    return this.AddProductForm.controls[controlName];
  }
  onSubmit() {
    debugger;
    console.log(this.AddProductForm.value)
    if (this.AddProductForm.valid) {
      this._productService.SaveProductMaster(this.AddProductForm.value).subscribe(result => {
        console.log(result);
        
      });
    }
    else {
      this.showErrorsOnInvalidForm();
    }
  }

  showErrorsOnInvalidForm() {
    Object.keys(this.AddProductForm.controls).forEach(field => {
      const control = this.AddProductForm.get(field);
      control.markAsTouched({ onlySelf: true });
    });
  }
}
