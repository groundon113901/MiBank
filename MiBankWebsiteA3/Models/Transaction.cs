using MiBankWebsiteA2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MiBankWebsiteA2.Models
{
    public class Transaction
    {

        [NotNull, StringLength(4)]
        [Description("Auto-generated unique ID for each transaction type")]
        public int TransactionID { get; set; }

        [NotNull]
        [Description("Type of transaction taking place")]
        [Required]
        public TransactionTypeEnum TransactionType { get; set; }

        [NotNull, StringLength(4)]
        [Description("Source account number")]
        [Required]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [StringLength(4), ForeignKey("DestAccount")]
        [Description("Destination Account - used for transfers")]
        public int? DestAccountNumber { get; set; }
        public virtual Account DestAccount { get; set; }

        [StringLength(8)]
        [Column(TypeName = "Money")]
        [Range(0, double.PositiveInfinity)]
        [Required]
        public decimal Amount { get; set; }

        [StringLength(50)]
        [Description("Any comments the banker added to the transaction")]
        [Required]
        public string Comment { get; set; }

        public DateTime TransactionTimeUtc { get; set; }

        [StringLength(8)]
        [Description("Last Date/Time record was modified")]
        public DateTime LastModified { get; set; }

        //Constructors
        
        public Transaction() { }
        public Transaction(int ID, TransactionTypeEnum transactionType, int sourceAccountID)
        {
            TransactionID = ID;
            TransactionType = transactionType;
            DestAccountNumber = sourceAccountID;
        }
        public Transaction(TransactionTypeEnum transactionType, decimal amount, String comment)
        {
            TransactionType = transactionType;
            Amount = amount;
            Comment = comment;
        }
        public Transaction(int ID, DateTime modified, decimal amount, TransactionTypeEnum transactionType, int sourceAccountID, int destinationAccountID, string comment) : this(ID, transactionType, sourceAccountID)
        {
            Amount = amount;
            DestAccountNumber = destinationAccountID;
            Comment = comment;
            LastModified = modified;
        }
        public Transaction(decimal amount, TransactionTypeEnum transactionType, int sourceAccountID, int destinationAccountID, string comment)
        {
            Amount = amount;
            TransactionType = transactionType;
            DestAccountNumber = sourceAccountID;
            DestAccountNumber = destinationAccountID;
            Comment = comment;
        }

        public Transaction(decimal amount, TransactionTypeEnum transactionType, int sourceAccountID, string comment)
        {
            Amount = amount;
            TransactionType = transactionType;
            DestAccountNumber = sourceAccountID;
            Comment = comment;
        }

        public Transaction(decimal amount, Account FromAccount, Account DestinationAccount)
        {
            Amount = amount;
            AccountNumber = FromAccount.AccountNumber;
            DestAccountNumber = DestinationAccount.AccountNumber;
        }

        public void ProcessDeposit(decimal amount, int AccountNumber)
        {

        }

        public void ProcessWithdrawal(decimal amount, int AccountNumber)
        {

        }


    }
}
