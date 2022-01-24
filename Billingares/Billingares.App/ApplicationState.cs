using Billingares.Base;
using Ujeby.Blazor.Base;

namespace Billingares.App
{
    public class ApplicationState : ApplicationStateBase
    {
        public Transaction[] Transactions { get; private set; }

        public void SetTransactions(Transaction[] transactions)
        {
            Transactions = transactions;
            NotifyStateChanged();
        }
	}
}
