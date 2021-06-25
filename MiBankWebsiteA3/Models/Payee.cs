using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MiBankWebsiteA2.Models
{
    public class Payee
    {

        [StringLength(4), NotNull]
        [Description("Auto-Generated unique ID")]
        public int PayeeID { get; set; }

        [NotNull]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [StringLength(50), NotNull]
        public string Name { get; set; }

        [StringLength(50)]
        [Description("Payee's Address")]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }
        public string State { get; set; }

        [RegularExpression("[0-9]{4}")]
        public int PostCode { get; set; }

        [RegularExpression("+614-[0-9]{4}-[0-9]{4}")]
        public string Phone { get; set; }

        public virtual List<BillPay> Billpays { get; set; }
    }
}