using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class BillPay
    {
        [NotNull, StringLength(4)]
        [Description("Auto-generated unique ID for each billpay created")]
        public int BillPayID { get; set; }

        [StringLength(4), ForeignKey("Account")]
        [Required(ErrorMessage = "From account is required")]
        public int? AccountNumber { get; set; }

        public virtual Account Account { get; set; }

        [StringLength(4)]
        public int? PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [NotNull]
        [StringLength(8)]
        [Range(0, double.PositiveInfinity)]
        [Required(ErrorMessage = " The Amount is required")]
        public decimal Amount { get; set; }

        [NotNull]
        [Required(ErrorMessage = " The Scheduled Date is required")]
        public DateTime ScheduleDate { get; set; }

        [NotNull]
        [Required(ErrorMessage = " The Pay Period is required")]
        public BillPayPeriod PayPeriod { get; set; }

        [AllowNull]
        public bool Blocked { get; set; }

        [NotNull]
        public DateTime LastModified { get; set; }

        public void updateBillPay() {
            switch (PayPeriod) {
                case BillPayPeriod.M:
                    ScheduleDate = ScheduleDate.AddMonths(1);
                    LastModified = DateTime.Now;
                    break;
                case BillPayPeriod.Q:
                    ScheduleDate = ScheduleDate.AddMonths(3);
                    LastModified = DateTime.Now;
                    break;
                case BillPayPeriod.Y:
                    ScheduleDate = ScheduleDate.AddYears(1);
                    LastModified = DateTime.Now;
                    break;
                default:
                    break;
            }
        }

        public void billPayTransaction() {
            var transaction = new Transaction();
            transaction.AccountNumber = (int)AccountNumber;
            transaction.Amount = Amount;
            transaction.Comment = "BillPay for" + Payee.Name;
            transaction.LastModified = DateTime.Now;
            transaction.TransactionTimeUtc = ScheduleDate;
            transaction.TransactionType = TransactionTypeEnum.B;
            Account.Transactions.Add(transaction);
            Account.Balance -= Amount;
        }

        public void BlockOrUnBlock() {
            Blocked = !Blocked;
            LastModified = DateTime.Now;
        }
    }
}
