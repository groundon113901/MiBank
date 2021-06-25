using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiBankWebsiteA2
{
    public enum AccountTypeEnum
    {
        //Savings
        S,
        //Checking
        C
    }
    public enum TransactionTypeEnum
    {
        //Credit (Deposit Money)
        D,
        //Debit (Withdraw Money)
        W,
        //Debit (Transfer money between accounts)
        T,
        //Debit (Service Charge)
        S,
        //Debit (BillPay)
        B
    }

    public enum BillPayPeriod 
    { 
        //Monthly
        M,
        //Quarterly
        Q,
        //Annually
        Y,
        //Once off
        S
    }

    public static class BillPayperiodExtensions {
        public static string ToFriendlyString(this BillPayPeriod payPeriod) {
            switch (payPeriod) {
                case BillPayPeriod.M:
                    return "Monthly";
                case BillPayPeriod.Q:
                    return "Quarterly";
                case BillPayPeriod.Y:
                    return "Annally";
                case BillPayPeriod.S:
                    return "Once off";
                default:
                    return "";
            }
        }
    }

    public static class TransactionTypeExtensions
    {
        public static string ToFriendlyString(this TransactionTypeEnum payPeriod)
        {
            switch (payPeriod)
            {
                case TransactionTypeEnum.D:
                    return "Deposit";
                case TransactionTypeEnum.B:
                    return "BillPay";
                case TransactionTypeEnum.S:
                    return "Service Charge";
                case TransactionTypeEnum.W:
                    return "Withdraw";
                case TransactionTypeEnum.T:
                    return "Transfer";
                default:
                    return "";
            }
        }
    }
    public static class AccountTypeExtensions
    {
        public static string ToFriendlyString(this AccountTypeEnum accountType)
        {
            switch (accountType)
            {
                case AccountTypeEnum.S:
                    return "Savings";
                case AccountTypeEnum.C:
                    return "Checking";
                default:
                    return "";
            }
        }
    }
}
