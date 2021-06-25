import { Component } from '@angular/core';
import { BillpayService, BillPayData } from '../../services/billpay.service';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-fetch-billpay',
    templateUrl: './fetch-billpay.component.html',
    styleUrls: ['./fetch-billpay.component.css']
})
/** fetch-billpay component*/
export class FetchBillpayComponent {
  billPayList: BillPayData[]
    /** fetch-billpay ctor */
  constructor(public http: HttpClient, private _billpayService: BillpayService) {
    this.getBillPays();
  }

  getBillPays() {
    this._billpayService.getBillPays().subscribe(data => this.billPayList = this.setPayPeriod(data));
  }

  setPayPeriod(data: any) {
    data.forEach(billPay => {
      switch (billPay.payPeriod) {
        case 0:
          billPay.payPeriodWord = "Monthly";
          break;
        case 1:
          billPay.payPeriodWord = "Quarterly";
          break;
        case 2:
          billPay.payPeriodWord = "Annualy";
          break;
        case 3:
          billPay.payPeriodWord = "Once off";
          break;
      }
    })
    return data;
  }

  blockOrUnblock(billPayID) {
    this._billpayService.blockOrUnblockBillPay(billPayID).subscribe((data) =>
    {
      this.getBillPays();
    }, error => console.error(error));
  }
}
