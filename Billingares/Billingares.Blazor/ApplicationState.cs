using Ujeby.Blazor.Base;

namespace Billingares.Blazor
{
    public class ApplicationState : ApplicationStateBase
    {
        public string ClientId { get; set; }

        public ApplicationState()
		{
            ClientId = Guid.NewGuid().ToString("N");
        }

        public Claim[] Claims { get; private set; } = Array.Empty<Claim>();
        public bool OptimizeTransactions { get; private set; }

        public void UpdateClaims(Claim[] claims, bool optimizeTransactions)
        {
            Claims = claims;
            OptimizeTransactions = optimizeTransactions;

            NotifyStateChanged();
        }
	}
}
