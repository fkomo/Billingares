using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public interface ITransactionsClient
	{
		Task<IEnumerable<Transaction>> List(string id, Claim[] claims, bool optimize);
	}
}
