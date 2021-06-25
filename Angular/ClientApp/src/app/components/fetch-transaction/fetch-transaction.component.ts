import { Component } from '@angular/core';
import { TransactionService, TransactionData } from '../../services/transaction.service';
import { HttpClient } from '@angular/common/http';
import { ChartOptions, ChartType, ChartDataSets } from "chart.js";
import { CustomerData, CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-fetch-transaction',
  templateUrl: './fetch-transaction.component.html',
  styleUrls: ['./fetch-transaction.component.css']
})
/** fetch-transactions component*/
export class FetchTransactionComponent {
  transList: TransactionData[];
  custList: CustomerData[];
  /** fetch-customer ctor */
  constructor(public http: HttpClient, private _transactionService: TransactionService, private _customerService: CustomerService) {
    this.getTransactions();
    this.getCustomers();
  }

  getTransactions() {
    this._transactionService.getTransactions().subscribe(data => {
      this.transList = this.setTransactionTypeNames(data);
      console.log(this.transList);
      this.getTransactioTypeCount(this.transList);
      this.getTransactionCount(this.transList);
      this.getTransactionTypeAmountTotal(this.transList);
    });
  }
  getCustomers() {
    this._customerService.getCustomers().subscribe(data => this.custList = data);
  }

  setTransactionTypeNames(data: any) {
    data.forEach(trans => {
      switch (trans.transactionType) {
        case 0:
          trans.transactionTypeName = "Deposit";
          break;
        case 1:
          trans.transactionTypeName = "WithDraw";
          break;
        case 2:
          trans.transactionTypeName = "Transfer";
          break;
        case 3:
          trans.transactionTypeName = "Service";
          break;
        case 4:
          trans.transactionTypeName = "BillPay";
          break;
      }
    })
    return data;
  }
  customer = null;
  account = null;
  accountType = null;
  transType = null;
  fromAmount = null;
  toAmount = null;
  fromDate = null;
  toDate = null;
  public isLoaded = false;
  public isLoaded2 = false;
  public isLoaded3 = false;
  public chartOptions: ChartOptions = {
    responsive: true,
    scales: {
      yAxes: [{
        offset:true
      }]
    },
    title: {
      display: true,
      text: "Total Count of Transactions Per Customer"
    }
  };
  public chartOptions2: ChartOptions = {
    title: {
      display: true,
      text: "Total Amount Per Transaction Type"
    }
  };
  public chartOptions3: ChartOptions = {
    title: {
      display: true,
      text: "Total Amount of Transactions Per Type"
    }}
  public chartType: ChartType = "bar";
  public chartType2: ChartType = "doughnut";
  public chartType3: ChartType = "polarArea";
  public chartLegend = true;
  public chart1Data: ChartDataSets[];
  public chart2Data: ChartDataSets[];
  public chart3Data = [];
  public chartLabels = ["Deposit", "WithDraw", "Transfer", "Service", "BillPay"];
  public chart1Labels = [];

  onChange(event: Event) {
    this.customer = (document.getElementById("customer") as HTMLInputElement).value;
    this.account = (document.getElementById("account") as HTMLInputElement).value;
    this.accountType = (document.getElementById("accountType") as HTMLInputElement).value;
    this.transType = (document.getElementById("transType") as HTMLInputElement).value;
    this.fromAmount = (document.getElementById("fromAmount") as HTMLInputElement).value;
    this.toAmount = (document.getElementById("toAmount") as HTMLInputElement).value;
    this.fromDate = (document.getElementById("fromDate") as HTMLInputElement).value;
    this.toDate = (document.getElementById("toDate") as HTMLInputElement).value;
    let data = this.filterData();
    this.getTransactioTypeCount(data);
    this.getTransactionCount(data);
    this.getTransactionTypeAmountTotal(data);
  }
  filterData() {
    let data = this.transList;
    data = this.checkCustomer(data);
    data = this.checkAccount(data);
    data = this.checkAccountType(data);
    data = this.checkTransType(data);
    data = this.checkFromAmount(data);
    data = this.checkToAmount(data);
    data = this.checkFromDate(data);
    data = this.checkToDate(data);
    return data;
  }
  checkCustomer(data: any) {
    let temp = [];
    if (this.customer && this.customer != "null" && this.customer != null) {
      data.forEach(trans => {
        if (trans.account.customerID == this.customer) {
          temp.push(trans);
        }
      })
      return temp;
    } else {
      return data;
    }
  }
  checkAccount(data: any) {
    let temp = [];
    if (this.account && this.account != "null" && this.account != null) {
      data.forEach(trans => {
        if (trans.accountNumber == this.account) {
          temp.push(trans);
        }
      })
      return temp;
    } else {
      return data;
    }
  }
  checkAccountType(data: any) {
    let temp = [];
    if (this.accountType && this.accountType != null && this.accountType != "null") {
      data.forEach(trans => {
        if (trans.account.accountType == this.accountType) {
          temp.push(trans);
        }
      })
      return temp;
    } else {
      return data;
    }
  }
  checkTransType(data: any) {
    let temp = [];
    if (this.transType && this.transType != null && this.transType != "null") {
      data.forEach(trans => {
        if (trans.transactionType == this.transType) {
          temp.push(trans);
        }
      })
      return temp;
    } else {
      return data;
    }
  }
  checkFromAmount(data: any) {
    let temp = [];
    if (this.fromAmount && this.fromAmount != null && this.fromAmount != "null") {
      data.forEach(trans => {
        if (trans.amount >= this.fromAmount) {
          temp.push(trans);
        }
      })
      return temp;
    } else {
      return data;
    }
  }
  checkToAmount(data: any) {
    let temp = [];
    if (this.toAmount && this.toAmount != null && this.toAmount != "null") {
      data.forEach(trans => {
        if (trans.amount <= this.toAmount) {
          temp.push(trans);
        }
      })
      return temp;
    } else {
      return data;
    }
  }
  checkFromDate(data: any) {
    let temp = [];
    if (this.fromDate && this.fromDate != null && this.fromDate != "null") {
      data.forEach(trans => {
        if (trans.transactionTimeUtc >= this.fromDate) {
          temp.push(trans);
        }
      })
      return temp;
    } else {
      return data;
    }
  }
  checkToDate(data: any) {
    let temp = [];
    if (this.toDate && this.toDate != null && this.toDate != "null") {
      data.forEach(trans => {
        if (trans.transactionTimeUtc <= this.toDate) {
          temp.push(trans);
        }
      })
      return temp;
    } else {
      return data;
    }
  }
  getTransactioTypeCount(data: any) {
    let count0 = 0;
    let count1 = 0;
    let count2 = 0;
    let count3 = 0;
    let count4 = 0;
    data.forEach(trans => {
      if (trans.transactionType == 0) {
        count0 += 1;
      }
      if (trans.transactionType == 1) {
        count1 += 1;
      }
      if (trans.transactionType == 2) {
        count2 += 1;
      }
      if (trans.transactionType == 3) {
        count3 += 1;
      }
      if (trans.transactionType == 4) {
        count4 += 1;
      }
    })
    this.chart3Data = [];
    this.chart3Data.push(count0);
    this.chart3Data.push(count1);
    this.chart3Data.push(count2);
    this.chart3Data.push(count3);
    this.chart3Data.push(count4);
    this.isLoaded3 = true;
  }
  getTransactionCount(data: any) {
    this.chart1Data = [];
    this.chart1Labels = [];
    let customers = this.getNumberOfCustomers(data);
    let d = [];
    customers.forEach(customer => {
      d.push({ "x": customer.customerName, "y": 0 });
    })
    data.forEach(trans => {
      for (var i in d) {
        if (d[i].x == trans.account.customer.name) {
          d[i].y += 1;
          break;
        }
      }
    })
    let values = [];
    for (var i in d) {
      values.push(d[i].y);
      this.chart1Labels.push(d[i].x);
    }
    this.chart1Data = [
      {
        label: "Total Count",
        data: values,
        backgroundColor: "rgba(255, 99, 132, 0.2)",
        borderColor: "rgba(255, 99, 132, 1)",
        borderWidth: 1
      }
    ]
    this.isLoaded = true;
  }
  getNumberOfCustomers(data: any) {
    let customers = [];
    data.forEach(trans => {
      if (customers.length != 0) {
        for (var i in customers) {
          if (customers[i].customerID == trans.account.customerID) {
            break;
          }
          customers.push({ "customerID": trans.account.customerID, "customerName": trans.account.customer.name });
          break;
        }
      } else {
        customers.push({ "customerID": trans.account.customerID, "customerName": trans.account.customer.name });
      }
    })
    return customers;
  }
  getTransactionTypeAmountTotal(data: any) {
    let count0 = 0;
    let count1 = 0;
    let count2 = 0;
    let count3 = 0;
    let count4 = 0;
    let values = [];
    data.forEach(trans => {
      if (trans.transactionType == 0) {
        count0 += trans.amount;
      }
      if (trans.transactionType == 1) {
        count1 += trans.amount;
      }
      if (trans.transactionType == 2) {
        count2 += trans.amount;
      }
      if (trans.transactionType == 3) {
        count3 += trans.amount;
      }
      if (trans.transactionType == 4) {
        count4 += trans.amount;
      }
    })
    this.chart2Data = [];
    values.push(count0);
    values.push(count1);
    values.push(count2);
    values.push(count3);
    values.push(count4);
    this.chart2Data = [
      {
        label: "Total Amount",
        data: values,
      }
    ]
    this.isLoaded2 = true;
  }
}
