import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";

export interface LoginData {
  loginID: number;
  customerID: number;
  passwordHash: string;
  modifyDate: string;
  blocked: boolean;
  customer: any;
}

@Injectable()
export class LoginService {
  myAppUrl: string = "";

  constructor(private _http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getLogins() {
    return this._http.get<LoginData[]>(this.myAppUrl + "api/Login/Index");
  }

  getLoginById(id: number) {
    return this._http.get<LoginData>(this.myAppUrl + "api/Login/Details/" + id);
  }

  updateLogin(Login) {
    return this._http.put(this.myAppUrl + "api/Login/Edit", Login)
  }

  lockOrUnlock(LoginID) {
     return this._http.put(this.myAppUrl + "api/Login/LockOrUnlock", LoginID)
  }
}
