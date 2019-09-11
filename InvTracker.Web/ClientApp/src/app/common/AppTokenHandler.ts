import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class AppTokenHandler {
  public access_token_key = 'access_token';
  access_token_value = '';
  public IsAuthenticated = this.getIsAuthenticated();
  loginUserInfo: UserInfo;


  // getSessionToken() {
  //    if (sessionStorage.length > 0) {
  //        this.access_token_value = sessionStorage.getItem(this.access_token_key);
  //    }
  //    if (this.access_token_value == null) {
  //        this.access_token_value = '';
  //    }
  //    return this.access_token_value;
  // }
  setSessionToken(loginData) {
    this.access_token_value = loginData.TokenID;
    this.IsAuthenticated = loginData.IsAuthenticated;
    sessionStorage.setItem('UserInfo', JSON.stringify(loginData));
    sessionStorage.setItem(this.access_token_key, this.access_token_value);
  }
  clearSessionToken() {
    this.access_token_value = '';
    this.IsAuthenticated = false;
    sessionStorage.removeItem('UserInfo');
    sessionStorage.removeItem(this.access_token_key);
  }
  getLoginUserInfo() {
    this.loginUserInfo = JSON.parse(sessionStorage.getItem('UserInfo'));

    return this.loginUserInfo;
  }

  private getIsAuthenticated() {
    if (sessionStorage.getItem('UserInfo') !== null) {
      this.loginUserInfo = JSON.parse(sessionStorage.getItem('UserInfo'));
      return this.loginUserInfo.IsAuthenticated;
    } else {
      return false;
    }
  }
}

export class UserInfo {
  FullName: string;
  IdentityDomain: string;
  IdentityName: string;
  IsAuthenticated: boolean;
  LotusUserEmailID: string;
  NetworkId: string;
  TokenID: string;
  UserAppRoles: string;
  UserCurrentRole: string;
  UserEmailID: string;
  UserGPN: string;
  UserHasAccess: boolean;
  UserImage: string;
}

@Injectable()
export class HTTPStatus {
  private requestInFlight$: BehaviorSubject<boolean>;
  constructor() {
    this.requestInFlight$ = new BehaviorSubject(false);
  }

  setHttpStatus(inFlight: boolean) {
    this.requestInFlight$.next(inFlight);
  }

  getHttpStatus(): Observable<boolean> {
    return this.requestInFlight$.asObservable();
  }

}
