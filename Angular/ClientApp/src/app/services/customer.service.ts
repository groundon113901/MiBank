import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";

export interface CustomerData {
  customerId: number;
  name: string;
  tfn: string;
  address: string;
  city: string;
  state: string;
  postCode: number;
  phone: string;
  account: any;
}

@Injectable()
export class CustomerService {
  myAppUrl: string = "";

  constructor(private _http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getCustomers() {
    return this._http.get<CustomerData[]>(this.myAppUrl + "api/Customer/Index");
  }

  getCustomerById(id: number) {
    return this._http.get<CustomerData>(this.myAppUrl + "api/Customer/Details/" + id);
  }

  updateCustomer(customer) {
    return this._http.put(this.myAppUrl + "api/Customer/Edit", customer);
  }
}
