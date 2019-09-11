import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/common.service';
import { Router, ActivatedRoute } from '@angular/router';

import { AppTokenHandler } from '../../common/AppTokenHandler';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private _commonService: CommonService, private router: Router, private activeRoute: ActivatedRoute,
    private tokenHandler: AppTokenHandler) { }

  ngOnInit() {

    this._commonService.demo().subscribe(data1 => {
      console.log(data1);
    })
    this._commonService.BindDropDown("ProdCat").subscribe(data1 => {
      console.log(data1);
    })

    //this._commonService.LoginUser().subscribe(data => {
    //  console.log(data);
    //  this._commonService.demo().subscribe(data1 => {
    //    console.log(data1);
    //  })
    //  if (data['RequestData'] != null) {
    //    if (data['RequestData'].IsAuthenticated) {
    //      this.SetSessionAndNavigate(data['RequestData']);
    //    }
    //  }
    //});
  }
  SetSessionAndNavigate(loginData) {
    this.tokenHandler.setSessionToken(loginData);
    this.router.navigate(['/dashboard']);
  }
}
