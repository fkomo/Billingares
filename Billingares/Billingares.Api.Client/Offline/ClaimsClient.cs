using Billingares.Api.Interfaces;
using Billingares.Base;

namespace Billingares.Api.Client.Offline
{
	public class ClaimsClient : IClaimsApi
	{
		public Task<IEnumerable<Claim>> List(string clientId)
		{
			return Task.FromResult(Array.Empty<Claim>().AsEnumerable());
		}

		public Task<IEnumerable<Claim>> Update(string clientId, Claim[] claims)
		{
			return Task.FromResult(claims.AsEnumerable());
		}

		public Task<Claim> Add(string clientId, Claim claim)
		{
			return Task.FromResult(claim);
		}
	}
}
