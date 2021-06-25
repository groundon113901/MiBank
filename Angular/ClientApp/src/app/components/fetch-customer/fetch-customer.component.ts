import { Component } from '@angular/core';
import { CustomerData, CustomerService } from '../../services/customer.service';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-fetch-customer',
    templateUrl: './fetch-customer.component.html',
    styleUrls: ['./fetch-customer.component.css']
})
/** fetch-customer component*/
export class FetchCustomerComponent {
  cusList: CustomerData[];

    /** fetch-customer ctor */
    constructor(public http: HttpClient, private _customerService: CustomerService) {
      this.getCustomers();
  }

  getCustomers() {
    this._customerService.getCustomers().subscribe(data => this.cusList = data);
  }

}
