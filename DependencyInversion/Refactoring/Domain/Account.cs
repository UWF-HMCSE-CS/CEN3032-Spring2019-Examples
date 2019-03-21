using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainInterfaces;

namespace Domain
{
    public abstract class Account : IAccountFactory
    {
        private Account()
        {

        }
            
        public Account makeAccount(AccountType type)
        {
            switch(type)
            {
                case AccountType.Silver:
                    return new SilverAccount();
                case AccountType.Gold:
                    return new GoldAccount();
                case AccountType.Platinum:
                    return new PlatinumAccount();
            }
            return new SilverAccount();
        }

        public decimal Balance
        {
            get;
            private set;
        }

        public int RewardPoints
        {
            get;
            private set;
        }

        public void AddTransaction(decimal amount)
        {
            RewardPoints += CalculateRewardPoints(amount);
            Balance += amount;
        }

        public abstract int CalculateRewardPoints(decimal amount);

        private readonly AccountType type;
    }
}
