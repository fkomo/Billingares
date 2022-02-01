using Billingares.Api.Interfaces;
using Billingares.Base;

namespace Billingares.Api.Client.Offline
{
	public class TransactionsClient : ITransactionsApi
	{
		public Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize)
		{
			var transactions = new TransactionBag().Add(claims, optimize);

			return Task.FromResult(transactions);
		}
	}
}
