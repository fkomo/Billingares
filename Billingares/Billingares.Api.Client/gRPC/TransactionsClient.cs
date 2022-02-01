using Billingares.Api.Interfaces;
using Billingares.Base;

namespace Billingares.Api.Client.gRPC
{
	public class TransactionsClient : GRPCClientBase, ITransactionsApi
	{
		public TransactionsClient(string baseUrl) : base(baseUrl)
		{
		}

		public async Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize)
		{
			throw new NotImplementedException();
		}
	}
}
