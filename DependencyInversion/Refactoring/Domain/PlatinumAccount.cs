using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class PlatinumAccount : Account
    {
        public override int CalculateRewardPoints(decimal amount)
        {
            int points = (int)decimal.Floor((Balance / PlatinumAccountBalanceRewardModifier * PlatinumAccountRewardModifier) 
                + (amount / PlatinumAccountRewardModifier));
            return points;
        }

        private int PlatinumAccountRewardModifier = 2;
        private int PlatinumAccountBalanceRewardModifier = 10000;
    }
}
