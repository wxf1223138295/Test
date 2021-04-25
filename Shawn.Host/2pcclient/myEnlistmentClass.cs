using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace _2pcclient
{
    public class myEnlistmentClass : IEnlistmentNotification
    {
        public void Commit(Enlistment enlistment)
        {
            throw new NotImplementedException();
        }

        public void InDoubt(Enlistment enlistment)
        {
            throw new NotImplementedException();
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            throw new NotImplementedException();
        }

        public void Rollback(Enlistment enlistment)
        {
            throw new NotImplementedException();
        }
    }
}
