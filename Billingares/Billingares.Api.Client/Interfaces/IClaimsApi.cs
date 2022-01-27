using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public interface IClaimsApi
	{
		Task<IEnumerable<Claim>> List(string clientId);

		Task<IEnumerable<Claim>> Update(string clientId, Claim[] claims);

		Task<Claim> Add(string clientId, Claim claim);
	}
}
