using Billingares.Api.Interfaces;
using Grpc.Net.Client;
using Ujeby.Api.Client.Base;

namespace Billingares.Api.Client.gRPC
{
	public class ClaimsClient : ClientBase, IClaimsApi
	{
		protected Interfaces.gRPC.Claims.ClaimsClient Client { get; set; }

		public ClaimsClient(string baseUrl) : base(baseUrl)
		{
		}

		public ClaimsClient(Interfaces.gRPC.Claims.ClaimsClient client) : base(null)
		{
			Client = client;
		}

		public async Task<IEnumerable<Claim>> List(string clientId)
		{
			if (Client == null && BaseUri != null)
			{
				//var channel = GrpcChannel.ForAddress(BaseUri);
				var channel = GrpcChannel.ForAddress(BaseUri,
					new GrpcChannelOptions
					{
						HttpHandler = new Grpc.Net.Client.Web.GrpcWebHandler(new HttpClientHandler())
					});
				Client = new Interfaces.gRPC.Claims.ClaimsClient(channel);
			}

			var response = await Client.ListAsync(new Interfaces.gRPC.ListRequest { ClientId = clientId });
			var claims = response.Claims.Select(c =>
				new Claim
				{
					Amount = (decimal)c.Amount,
					Creditor = c.Creditor,
					DebtorList = c.Debtors,
					Description = c.Description,
				});

			return claims;
		}

		public Task<IEnumerable<Claim>> Update(string clientId, Claim[] claims)
		{
			throw new NotImplementedException();
		}

		public Task<Claim> Add(string clientId, Claim claim)
		{
			throw new NotImplementedException();
		}
	}
}
