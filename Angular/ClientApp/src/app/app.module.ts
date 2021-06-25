import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ChartsModule } from 'ng2-charts';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { CustomerService } from "./services/customer.service";
import { FetchCustomerComponent } from "./components/fetch-customer/fetch-customer.component";
import { EditCustomerComponent } from "./components/edit-customer/edit-customer.component";

import { TransactionService } from "./services/transaction.service";
import { FetchTransactionComponent } from "./components/fetch-transaction/fetch-transaction.component";

import { LoginService } from "./services/login.service";
import { FetchLoginComponent } from "./components/fetch-login/fetch-login.component";

import { BillpayService } from "./services/billpay.service";
import { FetchBillpayComponent } from "./components/fetch-billpay/fetch-billpay.component";

import { TransactionSearchPipe } from "./Pipes/transaction-search-pipe.module"

import { UniquePipe } from "./Pipes/unique-pipe.module"

import { NgxChartsModule } from '@swimlane/ngx-charts';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchCustomerComponent,
    FetchTransactionComponent,
    FetchLoginComponent,
    FetchBillpayComponent,
    EditCustomerComponent,
    TransactionSearchPipe,
    UniquePipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: "fetch-customer", component: FetchCustomerComponent },
      { path: "fetch-transaction", component: FetchTransactionComponent },
      { path: "fetch-login", component: FetchLoginComponent },
      { path: "fetch-billpay", component: FetchBillpayComponent },
      { path: "customer/edit/:id", component: EditCustomerComponent }
    ]),
    ChartsModule
  ],
  providers: [CustomerService, TransactionService, LoginService, BillpayService],
  bootstrap: [AppComponent]
})
export class AppModule { }
