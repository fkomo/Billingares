using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public class TransactionsClient : ClientBase, ITransactionsClient
	{
		public TransactionsClient(string baseUrl) : base(baseUrl)
		{
		}

		public Task<IEnumerable<Transaction>> List(string id, Claim[] claims, bool optimize)
		{
			var request = new RequestEnvelope<Claim[]>(id, claims);
			return Post<RequestEnvelope<Claim[]>, IEnumerable<Transaction>>($"transactions?optimize={ optimize.ToString().ToLower() }", request);
		}
	}
}
