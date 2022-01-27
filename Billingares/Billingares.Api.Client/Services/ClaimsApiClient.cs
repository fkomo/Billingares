using Billingares.Base;
using Ujeby.Api.Client.Base;

namespace Billingares.Api.Client.Services
{
	public class ClaimsApiClient : ClientApiBase, IClaimsApi
	{
		public ClaimsApiClient(string baseUrl) : base(baseUrl)
		{
		}

		public Task<IEnumerable<Claim>> List(string clientId)
		{
			return Get<IEnumerable<Claim>>($"claims/list?{ nameof(clientId) }={ clientId }");
		}

		public Task<IEnumerable<Claim>> Update(string clientId, Claim[] claims)
		{
			return Post<Claim[], IEnumerable<Claim>>($"claims/update?{ nameof(clientId) }={ clientId }", claims);
		}

		public Task<Claim> Add(string clientId, Claim claim)
		{
			return Post<Claim, Claim>($"claims/add?{ nameof(clientId) }={ clientId }", claim);
		}
	}
}
