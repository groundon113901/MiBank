using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json.Serialization;
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
    public class Customer
    {
        [Range(0, 9999), NotNull]
        [Description("A unique ID for each customer")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }

        [StringLength(50), NotNull]
        public string Name { get; set; }

        [StringLength(11)]
        [Description("Tax File Number - This is just for identification purposes - no tax implacations")]
        public string TFN { get; set; }
        
        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(3)]
        [RegularExpression("[A-Za-z]{3}", ErrorMessage="State must be 3 letter abbreviation")]
        public string State { get; set; }

        [RegularExpression("[0-9]{4}", ErrorMessage = "Postcode must be 4 numbers")]
        [Range(0,9999), AllowNull]
        public int PostCode { get; set; }

        [StringLength(15), NotNull]
        [RegularExpression("[+]614-[0-9]{4}-[0-9]{4}", ErrorMessage = "Phone number must be in the format: +614-9999-9999")]
        public string Phone { get; set; }

        //Navigation properties
        public virtual List<Account> Accounts { get; set; }

        public virtual List<Payee> Payees { get; set; }

        private List<BillPay> getBillPays (List<Account> Accounts) {
            var listOfBillPays = new List<BillPay>();
            foreach (var account in Accounts){
                foreach (var billPay in account.BillPays) {
                    listOfBillPays.Add(billPay);
                }
            }
            return listOfBillPays;
        }

        public void updateBillPays() {
            var billpays = getBillPays(Accounts);
            foreach (var bill in billpays) {
                if (bill.ScheduleDate <= DateTime.Now)
                {
                    if (bill.PayPeriod == BillPayPeriod.M || bill.PayPeriod == BillPayPeriod.Q || bill.PayPeriod == BillPayPeriod.Y) {
                        if (!bill.Blocked)
                            bill.billPayTransaction();
                        bill.updateBillPay();
                    }
                }
            }
        } 
    }
}
