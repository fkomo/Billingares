using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public class OfflineTransactionsClient : ITransactionsClient
	{
		public Task<IEnumerable<Transaction>> List(string id, Claim[] claims, bool optimize)
		{
			var tBag = new TransactionBag();
			foreach (var claim in claims)
				foreach (var transaction in claim.Transactions)
					tBag.Add(transaction);
			var transactions = optimize ?
				tBag.Minimalize().ToArray() : tBag.Transactions.ToArray();

			return Task.FromResult(transactions.AsEnumerable());
		}
	}
}
