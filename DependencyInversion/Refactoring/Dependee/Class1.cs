using System;
using DomainInterfaces;

namespace Dependee
{
    public class Dependee
    {
        IAccountFactory accountFactory;

        Dependee(IAccountFactory accountFactory)
        {
        }
    }
}
