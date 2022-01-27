using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public interface ITransactionsApi
	{
		Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize);
	}
}
