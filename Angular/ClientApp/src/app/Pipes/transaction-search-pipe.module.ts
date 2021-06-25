import { Pipe, PipeTransform } from '@angular/core';
import { TransactionData, TransactionService } from '../services/transaction.service';

@Pipe({
  name: 'transasctionPipe'
})

export class TransactionSearchPipe implements PipeTransform {
  transform(items: Array<TransactionData>, customerSearch: any, amountFromSearch: any, amountToSearch: any, dateFromRange: any, dateToRange: any, accountSearch: any, accountTypeSearch: any, transactionTypeSearch: any) {
    if (items && items.length) {
      return items.filter(item => {
        if (customerSearch != "null" && customerSearch && customerSearch != item.account.customerID) {
          return false;
        }
        if (amountToSearch && item.amount > amountToSearch) {
          return false;
        }

        if (amountFromSearch && item.amount < amountFromSearch) {
          return false;
        }

        if (dateFromRange && item.transactionTimeUtc < dateFromRange) {
          return false;
        }

        if (dateToRange && item.transactionTimeUtc > dateToRange) {
          return false;
        }

        if (transactionTypeSearch && transactionTypeSearch != 'null' && transactionTypeSearch != item.transactionType) {
          return false;
        }

        if (accountSearch && accountSearch != 'null' && accountSearch != item.accountNumber) {
          return false;
        }

        if (accountTypeSearch && accountTypeSearch != 'null' && accountTypeSearch != item.account.accountType) {
          return false;
        }
        return true;
      })
    }
    else {
      return items;
    }
  }
}
