using Billingares.Api.Interfaces;
using Ujeby.Api.Client.Base;

namespace Billingares.Api.Client.gRPC
{
	public class InfoClient : ClientBase, IInfoApi
	{
		public InfoClient(string baseUrl) : base(baseUrl)
		{
		}

		public Task<string> Version()
		{
			return Task.FromResult(string.Empty);
		}
	}
}
