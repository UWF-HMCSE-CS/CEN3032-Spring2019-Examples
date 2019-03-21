using System;
using System.Collections.Generic;
using System.Text;

namespace DomainInterfaces
{
    public interface IAccount
    {
        int CalculateRewardPoints(decimal amount);
    }
}
