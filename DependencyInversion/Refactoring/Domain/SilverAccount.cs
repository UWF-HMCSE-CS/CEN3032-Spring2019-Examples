using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class SilverAccount : Account
    {
        public override int CalculateRewardPoints(decimal amount)
        {
            int points = (int)decimal.Floor(amount / SilverAccountRewardModifier);
            return points;
        }

        private int SilverAccountRewardModifier = 10;
    }
}
