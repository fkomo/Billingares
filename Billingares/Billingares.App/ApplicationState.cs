using Billingares.Base;
using Ujeby.Blazor.Base;

namespace Billingares.App
{
    public class ApplicationState : ApplicationStateBase
    {
        public string ClientId { get; set; }

        public ApplicationState()
		{
#if DEBUG
            ClientId = "debug";
#else
            ClientId = Guid.NewGuid().ToString("N");
#endif
        }

        public Transaction[] Transactions { get; private set; }

        public void SetTransactions(Transaction[] transactions)
        {
            Transactions = transactions;
            NotifyStateChanged();
        }
	}
}
