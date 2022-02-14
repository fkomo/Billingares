namespace Billingares.Api.Interfaces
{
	public interface ISessionsApi
	{
		Task<IEnumerable<Session>> List();
	}
}
