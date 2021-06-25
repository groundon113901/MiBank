import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";

export interface BillPayData {
  billPayID: number;
  accountNumber: number;
  payeeID: number;
  amount: number;
  scheduleDate: string;
  payPeriod: number;
  blocked: boolean;
  lastModified: string;
  payee: any;
  account: any;
  payPeriodWord: any;
}

@Injectable()
export class BillpayService {
  myAppUrl: string = "";

  constructor(private _http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getBillPays() {
    return this._http.get<BillPayData[]>(this.myAppUrl + "api/BillPay/Index");
  }

  blockOrUnblockBillPay(BillPayID) {
    return this._http.put(this.myAppUrl + "api/BillPay/BlockOrUnblock", BillPayID)
  }
}
