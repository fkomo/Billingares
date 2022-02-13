using Billingares.Api.Interfaces;
using Ujeby.Api.Client.Base;

namespace Billingares.Api.Client.gRPC
{
	public class TransactionsClient : ClientBase, ITransactionsApi
	{
		public TransactionsClient(string baseUrl) : base(baseUrl)
		{
		}

		public Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize)
		{
			throw new NotImplementedException();
		}
	}
}
