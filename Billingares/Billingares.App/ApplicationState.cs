﻿using Billingares.Base;
using Ujeby.Blazor.Base;

namespace Billingares.App
{
    public class ApplicationState : ApplicationStateBase
    {
        public string ClientId { get; private set; }

        public ApplicationState()
		{
            ClientId = Guid.NewGuid().ToString("N");
        }

        public Transaction[] Transactions { get; private set; }

        public void SetTransactions(Transaction[] transactions)
        {
            Transactions = transactions;
            NotifyStateChanged();
        }
	}
}
