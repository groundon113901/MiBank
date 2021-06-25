using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MiBankWebsiteA2.Models
{
    public class Account
    {
        //Database Tables
        [NotNull, Required, Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountNumber { get; set; }
        [NotNull]
        public AccountTypeEnum AccountType { get; set; }
        [NotNull, ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [NotNull]
        public DateTime LastModified { get; set; }

        public decimal Balance { get; set; }
        
        [NotMapped]
        public int FreeTransactions { get; } = 4;

        //Navigation Properties
        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<BillPay> BillPays { get; set; }

        //Service Fees
        public decimal ATMfee { get; } = 0.1m;
        public decimal Transferfee { get; } = 0.2m;
        public string AtmFeeComment { get; } = "Atm withdrawl fee";
        public string TransferFeeComment { get; } = "Transfer fee";

        //Logic
        public Transaction AtmFeeTransaction()
        {
            return new Transaction(ATMfee, TransactionTypeEnum.S, this.AccountNumber, AtmFeeComment);
        }

        public Transaction TransferFeeTransaction()
        {
            return new Transaction(Transferfee, TransactionTypeEnum.S, this.AccountNumber, TransferFeeComment);
        }

        public Boolean FeeRequired(Account sourceAccount)
        {
            var count = 0;
            foreach (Transaction transaction in sourceAccount.Transactions)
            {
                if (transaction.TransactionType == TransactionTypeEnum.T || transaction.TransactionType == TransactionTypeEnum.W || transaction.TransactionType == TransactionTypeEnum.B)
                {
                    count += 1;
                }
            }
            if (count > sourceAccount.FreeTransactions)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void deposit(decimal amount, string comment) {
            Balance += amount;
            Transactions.Add(
                new Transaction
                {
                    AccountNumber = AccountNumber,
                    Comment = comment,
                    TransactionType = TransactionTypeEnum.D,
                    Amount = amount,
                    TransactionTimeUtc = DateTime.UtcNow
                });
        }

        public void withdraw(decimal amount, string comment) {
            //Transfer Fee
            if (FeeRequired(this))
            {
                Transactions.Add(
                    new Transaction
                    {
                        AccountNumber = AccountNumber,
                        Comment = AtmFeeComment,
                        Amount = ATMfee,
                        TransactionTimeUtc = DateTime.UtcNow,
                    });
                amount -= ATMfee;
            }

            Balance -= amount;
            Transactions.Add(
                new Transaction
                {
                    AccountNumber = AccountNumber,
                    Comment = comment,
                    TransactionType = TransactionTypeEnum.W,
                    Amount = amount,
                    TransactionTimeUtc = DateTime.UtcNow
                });
        }

        public void transfer(decimal amount, string comment, Account destAccount)
        {
            //Transfer Fee
            if (FeeRequired(this))
            {
                Transactions.Add(
                    new Transaction
                    {
                        AccountNumber = AccountNumber,
                        Comment = TransferFeeComment,
                        Amount = Transferfee,
                        TransactionTimeUtc = DateTime.UtcNow,
                    });
                amount -= Transferfee;
            }

            Balance -= amount;
            destAccount.Balance += amount;
            Transactions.Add(
                new Transaction
                {
                    AccountNumber = AccountNumber,
                    Comment = comment,
                    TransactionType = TransactionTypeEnum.T,
                    Amount = amount,
                    TransactionTimeUtc = DateTime.UtcNow,
                    DestAccountNumber = destAccount.AccountNumber
                }
            );

            destAccount.Transactions.Add(
                new Transaction
                {
                    AccountNumber = destAccount.AccountNumber,
                    Comment = comment,
                    TransactionType = TransactionTypeEnum.T,
                    Amount = amount,
                    TransactionTimeUtc = DateTime.UtcNow
                }
                );
        }
    }
}
