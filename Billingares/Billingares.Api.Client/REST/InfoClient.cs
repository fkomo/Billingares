using Billingares.Api.Interfaces;
using Ujeby.Api.Client.Base;

namespace Billingares.Api.Client.REST
{
	public class InfoClient : RESTClientBase, IInfoApi
	{
		public InfoClient(string baseUrl) : base(baseUrl)
		{
		}

		public async Task<string> Version()
		{
			var info = await Get<AppInfo>();
			return info != null ? $"{ info.Name } { info.Version }" : null;
		}
	}
}
