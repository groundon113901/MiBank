using MiBankWebsiteA2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MiBankWebsiteA2.Data
{
    public class MiBankContext : DbContext
    {
        public MiBankContext(DbContextOptions<MiBankContext> options) : base(options)
        {
        }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(nameof(MiBankContext));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Constraints for Customers
            builder.Entity<Customer>().HasCheckConstraint("CH_Customer_CustomerID", "len(CustomerID) = 4")
                                        .HasCheckConstraint("CH_Customer_Name", "len(Name) <= 50")
                                        .HasCheckConstraint("CH_Customer_FTN", "len(TFN) <= 11")
                                        .HasCheckConstraint("CH_Customer_Address", "len(Address) <= 50")
                                        .HasCheckConstraint("CH_Customer_City", "len(City) <= 40")
                                        .HasCheckConstraint("CH_Customer_Phone", "len(Phone) <= 15");
            //Constraints for Login
            builder.Entity<Login>().HasCheckConstraint("CH_Login_LoginID", "len(LoginID) = 8")
                                    .HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64")
                                    .HasCheckConstraint("CH_Login_CustomerID", "len(CustomerID) = 4");
            //Constraints for Accounts
            builder.Entity<Account>().HasCheckConstraint("CH_Account_Balance", "Balance >= 0")
                                        .HasCheckConstraint("CH_Account_CustomerID", "len(CustomerID) = 4");
            //Constraints for Transactions
            builder.Entity<Transaction>().HasOne(x => x.Account).WithMany(x => x.Transactions).HasForeignKey(x => x.AccountNumber);           
            builder.Entity<Transaction>().HasCheckConstraint("CH_Transaction_Comment", "Len(Comment) <= 50");

        }

        public DbSet<BillPay> BillPays { get; set; }

        public DbSet<Payee> Payees { get; set; }
        public object HttpContext { get; internal set; }
    }
}
