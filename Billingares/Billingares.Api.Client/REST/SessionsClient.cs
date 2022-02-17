using Billingares.Api.Interfaces;
using Ujeby.Api.Client.Base;

namespace Billingares.Api.Client.REST
{
	public class SessionsClient : RESTClientBase, ISessionsApi
	{
		public SessionsClient(string baseUrl) : base(baseUrl)
		{
		}

		public Task<IEnumerable<Session>> List()
		{
			return Get<IEnumerable<Session>>($"sessions/list");
		}
	}
}
