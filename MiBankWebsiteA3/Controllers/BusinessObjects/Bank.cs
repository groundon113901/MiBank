using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiBankWebsiteA2.Models
{
    public class Bank
    {
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


    }
}
