namespace Billingares.Api.Interfaces
{
	public interface ITransactionsApi
	{
		Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize);
	}
}
