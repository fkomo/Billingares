using Billingares.Api.Interfaces;

namespace Billingares.Api.Client.gRPC
{
	public class InfoClient : GRPCClientBase, IInfoApi
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
