import { Injectable } from '@angular/core';
import { DataService } from '../core/data.service';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ProductMasterService {

  constructor(private _dataService: DataService) { }

  SaveProductMaster(ProductMaster: any): Observable<any> {
    return this._dataService.create("Product/SaveProductMaster", ProductMaster)
  }
  GetAllProductMaster(ViewProductFormData: any): Observable<any> {
    return this._dataService.getAllUsingPost("Product/GetProductMasters", ViewProductFormData)
  }
}
