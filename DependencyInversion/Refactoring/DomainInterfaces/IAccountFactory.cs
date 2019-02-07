using System;
using System.Collections.Generic;
using System.Text;

namespace DomainInterfaces
{
    public interface IAccountFactory
    {
        IAccount makeAccount(AccountType type);
    }
}
