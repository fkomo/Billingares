namespace Billingares.Api.Client.gRPC
{
	public abstract class GRPCClientBase
	{
		protected string BaseUrl { get; set; }

		public GRPCClientBase(string baseUrl)
		{
			BaseUrl = baseUrl;
		}
	}
}