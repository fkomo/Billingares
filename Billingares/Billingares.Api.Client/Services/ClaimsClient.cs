
using Billingares.Api.Client.Base;
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
			return Get<IEnumerable<Claim>>($"api/claims/{ id }");
		}

		public Task<IEnumerable<Claim>> Update(string id, Claim[] items)
		{
			return Post<Claim[], IEnumerable<Claim>>($"api/claims/{ id }", items);
		}

		public Task<Claim> Add(string id, Claim item)
		{
			return Post<Claim, Claim>($"api/claim/{ id }", item);
		}
	}
}
