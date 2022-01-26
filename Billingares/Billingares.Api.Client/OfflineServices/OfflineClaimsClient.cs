using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public class OfflineClaimsClient : IClaimsClient
	{
		public Task<IEnumerable<Claim>> List(string id)
		{
			return Task.FromResult(Array.Empty<Claim>().AsEnumerable());
		}

		public Task<IEnumerable<Claim>> Update(string id, Claim[] claims)
		{
			return Task.FromResult(claims.AsEnumerable());
		}

		public Task<Claim> Add(string id, Claim claim)
		{
			return Task.FromResult(claim);
		}
	}
}
