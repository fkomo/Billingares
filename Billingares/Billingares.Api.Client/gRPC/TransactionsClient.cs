using Billingares.Api.Interfaces;

namespace Billingares.Api.Client.gRPC
{
	public class TransactionsClient : GRPCClientBase, ITransactionsApi
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
