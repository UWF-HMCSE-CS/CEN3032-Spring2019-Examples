using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class GoldAccount : Account
    {

        public override int CalculateRewardPoints(decimal amount)
        {
            int points = (int)decimal.Floor((Balance / GoldAccountBalanceRewardModifier * GoldAccountRewardModifier) 
                + (amount / GoldAccountRewardModifier));
            return points;
        }

        private int GoldAccountRewardModifier = 5;
        private int GoldAccountBalanceRewardModifier = 10000;
    }
}
