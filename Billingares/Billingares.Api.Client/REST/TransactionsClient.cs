using Billingares.Api.Interfaces;
using Ujeby.Api.Client.Base;

namespace Billingares.Api.Client.REST
{
	public class TransactionsClient : RESTClientBase, ITransactionsApi
	{
		public TransactionsClient(string baseUrl) : base(baseUrl)
		{
		}

		public Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize)
		{
			return Post<Claim[], IEnumerable<Transaction>>
				($"transactions/list?{ nameof(clientId) }={ clientId }&{ nameof(optimize) }={ optimize }", claims);
		}
	}
}
