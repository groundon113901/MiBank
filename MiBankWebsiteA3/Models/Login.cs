using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MiBankWebsiteA2.Models
{
    public class Login

    {
        [NotNull]
        [StringLength(8), Required(ErrorMessage = "Login ID is required")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Login ID must only contain numbers and be 8 characters long")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LoginID { get; set; }

        [NotNull, ForeignKey("Customer")]
        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        [NotNull]
        [Required(ErrorMessage = "Password is Required")]
        public string PasswordHash { get; set; }

        [NotNull]
        [StringLength(8)]
        public DateTime ModifyDate { get; set; }

        [AllowNull]
        public bool Locked { get; set; }

        public void LockOrUnlock() {
            Locked = !Locked;
            ModifyDate = DateTime.Now;
        }
    }
}
