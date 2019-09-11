import { Injectable } from '@angular/core';
import { DataService } from '../core/data.service'
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { MatSelectTrigger } from '@angular/material';


@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private _data: DataService, private http: HttpClient) { }


  private httpOptions = {
    headers: new HttpHeaders({
      //"Authorization": "Bearer mPlzHGQ-zRsNcj8sPeZxMFHtQSd4fccooZdVPJJJyglA2kjvFCsJGYn2iHR2DhoSISnIwJHrtb3Gpa06rY4Z9pBwv-6irPAkFTvHG9p4Hmep8FblxnRTNqA_bkjKf8W4maQEeXZH24Sg_OLcYZnmDLlz27FxRwNnJMt3jeU5ZyLxDP088_ffQRfOWVKh94d9o9-n-LUVi3dNZZ2_5pDvCqEN0Ekr_ZPFYg9nJA4Ctos3a9gPZF-DSRUBxmzmLFQtvWUVbyavfSni0eHUDqFm4LmjN36GpP6HMg4AhjQKZuy2GEVqHWY2pURb5CIkXS4NY1rOYipks8HAe_tyRz0MJB9hp16WlSoPjRpx5KQ9mOf52nIJKZKxviZuKs4HwdYXQh_PHHMDN1cFSZfJ57heZb7G2CKav81x2FE6PMS1DVBX4i5-P0C3Je_W5Fn5eTjR7qwUmMhet4RTNRiWLKxPGw"
    })
    //,'withCredentials' : true 
  };


  //private userData = "username=Shan&password=user123456&grant_type=password";
  private userData = "username=ShanM&password=123456&grant_type=password";
  private reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded', 'No-Auth': 'True' });


  LoginUser() {
    //return this.http.post('http://localhost:4312/oauth/token', this.userData, { headers: this.reqHeader })
    return this.http.get('http://localhost:4312/oauth/token', this.httpOptions)
      .pipe();

  }

  demo() {

    return this.http.get(environment.serviceBaseUrl + 'api/TestMethod', this.httpOptions)
      .pipe();
  }

  LogoutUser() {
    return this.http.get(environment.serviceBaseUrl + 'login/LogoutUser', this.httpOptions)
      .pipe();
  }

  BindDropDown(Flag: string, Param1?: string, Param2?: string, ) {
    return this._data.create('Common/BindDropDown', { Flag: Flag, Param1: Param1, Param2: Param2 });
  }
}
