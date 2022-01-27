using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public class OfflineTransactionsClient : ITransactionsApi
	{
		public Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize)
		{
			var transactions = new TransactionBag().Add(claims, optimize);

			return Task.FromResult(transactions);
		}
	}
}
