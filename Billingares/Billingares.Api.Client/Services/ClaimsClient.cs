using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public class ClaimsClient : ClientBase, IClaimsClient
	{
		public ClaimsClient(string baseUrl) : base(baseUrl)
		{
		}

		public Task<IEnumerable<Claim>> List(string id)
		{
			return Get<IEnumerable<Claim>>($"claims/{ id }");
		}

		public Task<IEnumerable<Claim>> Update(string id, Claim[] claims)
		{
			var request = new RequestEnvelope<Claim[]>(id, claims);
			return Post<RequestEnvelope<Claim[]>, IEnumerable<Claim>>($"claims", request);
		}

		public Task<Claim> Add(string id, Claim claim)
		{
			var request = new RequestEnvelope<Claim>(id, claim);
			return Post<RequestEnvelope<Claim>, Claim>($"claim", request);
		}
	}
}
