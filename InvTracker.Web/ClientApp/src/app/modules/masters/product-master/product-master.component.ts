import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ProductMasterService } from '../../../shared/product-master.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../../shared/common.service';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { merge, of as observableOf } from 'rxjs';
import { startWith, catchError, switchMap, map } from 'rxjs/operators';
import { createElementCssSelector } from '@angular/compiler';

@Component({
  selector: 'app-product-master',
  templateUrl: './product-master.component.html',
  styleUrls: ['./product-master.component.css']
})
export class ProductMasterComponent implements OnInit, AfterViewInit {


  ProductCategoryList: any;
  ProductSubCategoryList: any;
  ProductCompanyList: any;
  ViewProductForm: FormGroup;

  displayedColumns: any[]; //string[] = ['ProductCode', 'ProductName', 'ProductCategoryName', 'ProductSubCategoryName'];

  dataProductMaster: any[]; //MatTableDataSource<any[]>;

  totalLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private _productService: ProductMasterService, private fb: FormBuilder, private _commonService: CommonService) {
    this.ViewProductForm = this.fb.group({
      PageNumber: [0],
      RecordSize :[10],
      ProductCategoryId: [null, [Validators.required]],
      ProductSubCategoryId: [],
      ProductCode: [null, [Validators.required]],
      ProductName: [],
      ProductCompanyId: []
    });
  }



  ngOnInit() {
    this._commonService.BindDropDown('ProdCat').subscribe(data => {
      this.ProductCategoryList = data;
    });
    this._commonService.BindDropDown('ProdComp').subscribe(data => {
      this.ProductCompanyList = data;
    });
    this.getProductSubCategory(null);


    this.displayedColumns = [
      { field: 'ProductCode', header: 'Product Code' },
      { field: 'ProductName', header: 'Product Name' },
      { field: 'ProductCategoryName', header: 'Product Category Name' },
      { field: 'ProductSubCategoryName', header: 'Sub Category Name' }
    ];
  }
  ngAfterViewInit(): void {

    //this.getProductMaster();
  }

  getProductMaster(pageNumber) {
   

    //in a real application, make a remote request to load data using state metadata from event
    //event.first = First row offset (PageNumber)
    //event.rows = Number of rows per page
    //event.sortField = Field name to sort with
    //event.sortOrder = Sort order as number, 1 for asc and -1 for dec
    //filters: FilterMetadata object having field as key and filter value, filter matchMode as value
    this.isLoadingResults = true;
    this.ViewProductForm.controls.PageNumber.setValue(pageNumber);
    this._productService.GetAllProductMaster(this.ViewProductForm.value).subscribe(data => {
      console.log(data);
      if (data.DataResult.length > 0) {
        this.totalLength = data.DataResult[0].TotalRecords;
      }
      else {
        this.totalLength = 0;
      }
      
      this.dataProductMaster = data.DataResult;
      this.isLoadingResults = false;
    });
   
  }
  

  //getProductMaster() {
  //  // If the user changes the sort order, reset back to the first page.
  //  this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

  //  merge(this.sort.sortChange, this.paginator.page)
  //    .pipe(
  //      startWith({}),
  //      switchMap(() => {
  //        this.isLoadingResults = true;
  //        return this._productService.GetAllProductMaster(this.ViewProductForm.value)
  //      }),
  //      map(data => {
  //        // Flip flag to show that loading has finished.
  //        this.isLoadingResults = false;
  //        this.isRateLimitReached = false;
  //        this.resultsLength = data.TotalRecords;
  //        debugger;
  //        return data.DataResult;
  //      }),
  //      catchError(() => {
  //        this.isLoadingResults = false;
  //        // Catch if the GitHub API has reached its rate limit. Return empty data.
  //        this.isRateLimitReached = true;
  //        return observableOf([]);
  //      })
  //    ).subscribe(data => this.dataProductMaster = data);
  //}

  getProductSubCategory(ProductCategoryId) {
    this._commonService.BindDropDown('ProdSubCat', ProductCategoryId).subscribe(data => {
      this.ProductSubCategoryList = data;
    });
  }

  reset() {
    if (confirm('Are you sure want to reset from the form.')) {
      this.ViewProductForm.reset();
      this.dataProductMaster = [];

      this.ViewProductForm.controls.PageNumber.setValue(0);
      this.ViewProductForm.controls.RecordSize.setValue(10);
      this.totalLength = 0;
      //this.dataProductMaster = new MatTableDataSource(null);
      //  this.getProductMaster();
    }
  }

  exit() {
    if (confirm("Are you sure to exit?")) {
      // this._router.navigate(['dashboard']);
      window.history.back();
    }
  }

}
