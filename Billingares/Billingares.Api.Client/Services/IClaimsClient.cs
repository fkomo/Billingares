using Billingares.Base;

namespace Billingares.Api.Client.Services
{
	public interface IClaimsClient
	{
		Task<IEnumerable<Claim>> List(string id);

		Task<IEnumerable<Claim>> Update(string id, Claim[] claims);

		Task<Claim> Add(string id, Claim claim);
	}
}
