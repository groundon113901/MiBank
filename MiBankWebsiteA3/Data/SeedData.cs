using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using MiBankWebsiteA2.Models;

//This is not written by Mark or Justin.
//Script taken from weekly tute 7 and modified to suit our purposes for the sake of time efficiency.

namespace MiBankWebsiteA2.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MiBankContext(serviceProvider.GetRequiredService<DbContextOptions<MiBankContext>>());
            const string openingBalance = "Opening balance";
            const string format = "dd/MM/yyyy hh:mm:ss tt";
            // Look for customers.
            if (context.Customers.Any())
                return; // DB has already been seeded.

            context.Customers.AddRange(
                new Customer
                {
                    CustomerID = 2100,
                    Name = "Matthew Bolger",
                    Address = "123 Fake Street",
                    City = "Melbourne",
                    PostCode = 3000
                },
                new Customer
                {
                    CustomerID = 2200,
                    Name = "Rodney Cocker",
                    Address = "456 Real Road",
                    City = "Melbourne",
                    PostCode = 3005
                },
                new Customer
                {
                    CustomerID = 2300,
                    Name = "Shekhar Kalra",
                    PostCode = 3006
                });
            

            context.Logins.AddRange(
                new Login
                {
                    LoginID = 12345678,
                    CustomerID = 2100,
                    PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2"
                },
                new Login
                {
                    LoginID = 38074569,
                    CustomerID = 2200,
                    PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04"
                },
                new Login
                {
                    LoginID = 17963428,
                    CustomerID = 2300,
                    PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE"
                });
            

            context.Accounts.AddRange(
                new Account
                {
                    AccountNumber = 4100,
                    AccountType = AccountTypeEnum.S,
                    CustomerID = 2100,
                    Balance = 527.75m
                },
                new Account
                {
                    AccountNumber = 4101,
                    AccountType = AccountTypeEnum.C,
                    CustomerID = 2100,
                    Balance = 500
                },
                new Account
                {
                    AccountNumber = 4200,
                    AccountType = AccountTypeEnum.S,
                    CustomerID = 2200,
                    Balance = 500.95m
                },
                new Account
                {
                    AccountNumber = 4300,
                    AccountType = AccountTypeEnum.C,
                    CustomerID = 2300,
                    Balance = 1250.50m
                });
            

            context.Transactions.AddRange(
                //First Transaction rectified account creation
                new Transaction
                {
                    TransactionType = TransactionTypeEnum.D,
                    AccountNumber = 4100,
                    Amount = 52.75m,
                    Comment = "Initial Deposit.",
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null),
                    DestAccountNumber = 4100
                },
                new Transaction
                {
                    TransactionType = TransactionTypeEnum.D,
                    AccountNumber = 4100,
                    Amount = 75.00m,
                    Comment = "SeedDepositTwo",
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null),
                    DestAccountNumber = 4100
                },
                new Transaction
                {
                    TransactionType = TransactionTypeEnum.D,
                    AccountNumber = 4100,
                    Amount = 100m,
                    Comment = "SeedDepositThree",
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null),
                    DestAccountNumber = 4100
                },
                new Transaction
                {
                    TransactionType = TransactionTypeEnum.D,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = "SeedDepositFour",
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null),
                    DestAccountNumber = 4100
                },
                new Transaction
                {
                    TransactionType = TransactionTypeEnum.D,
                    AccountNumber = 4100,
                    Amount = 200,
                    Comment = "SeedDepositFive",
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null),
                    DestAccountNumber = 4100
                },
                //These transactions dont need to modify account balance.                
                new Transaction
                {
                    TransactionType = TransactionTypeEnum.D,
                    AccountNumber = 4101,
                    Amount = 500,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:30:00 PM", format, null),
                    DestAccountNumber = 4100
                },
                new Transaction
                {
                    TransactionType = TransactionTypeEnum.D,
                    AccountNumber = 4200,
                    Amount = 500.95m,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 09:00:00 PM", format, null),
                    DestAccountNumber = 4200
                },
                new Transaction
                {
                    TransactionType = TransactionTypeEnum.D,
                    AccountNumber = 4300,
                    Amount = 1250.50m,
                    Comment = "Opening balance",
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 10:00:00 PM", format, null),
                    DestAccountNumber = 4300
                });
            

            context.Payees.AddRange(
                new Payee
                {
                    Name = "John Doe",
                    Address = "456 Real Road",
                    City = "Melbourne",
                    PostCode = 3005,
                    CustomerID = 2100
                },
                 new Payee
                 {
                     Name = "Doe John",
                     Address = "654 Real Road",
                     City = "Melbourne",
                     PostCode = 3005,
                     CustomerID = 2100
                 },
                 new Payee
                 {
                     Name = "James Smith",
                     Address = "456 State Road",
                     City = "Melbourne",
                     PostCode = 3134,
                     CustomerID = 2200
                 }
                );
            

            context.BillPays.AddRange(
            new BillPay
            {
                PayeeID = 3,
                AccountNumber = 4200,
                Amount = 100,
                ScheduleDate = DateTime.ParseExact("28/12/2020 08:30:00 PM", format, null),
                PayPeriod = BillPayPeriod.Y
            },
            new BillPay
            {
                PayeeID = 2,
                AccountNumber = 4101,
                Amount = 100,
                ScheduleDate = DateTime.ParseExact("28/12/2020 08:30:00 PM", format, null),
                PayPeriod = BillPayPeriod.Y
            },
            new BillPay
            {
                PayeeID = 1,
                AccountNumber = 4100,
                Amount = 100,
                ScheduleDate = DateTime.ParseExact("28/12/2020 08:30:00 PM", format, null),
                PayPeriod = BillPayPeriod.Q
            },
            new BillPay
            {
                PayeeID = 2,
                AccountNumber = 4100,
                Amount = 100,
                ScheduleDate = DateTime.ParseExact("28/12/2020 08:30:00 PM", format, null),
                PayPeriod = BillPayPeriod.M
            }
            );

            context.SaveChanges();
        }
    }
}
