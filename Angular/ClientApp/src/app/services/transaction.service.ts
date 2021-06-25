import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";

export interface TransactionData {
  transactionID: number;
  accountNumber: number;
  destAccountNumber: number;
  transactionType: any;
  amount: number;
  comment: string;
  transactionTimeUtc: string;
  lastmodified: string;
  account: any;
  transactionTypeName: string;
}

@Injectable()
export class TransactionService {
  myAppUrl: string = "";

  constructor(private _http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getTransactions() {
    return this._http.get<TransactionData[]>(this.myAppUrl + "api/Transaction/Index");
  }
}
