
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
			return Get<IEnumerable<Claim>>($"claims/{ id }");
		}

		public Task<IEnumerable<Claim>> Update(string id, Claim[] items)
		{
			return Post<Claim[], IEnumerable<Claim>>($"claims/{ id }", items);
		}

		public Task<Claim> Add(string id, Claim item)
		{
			return Post<Claim, Claim>($"claim/{ id }", item);
		}
	}
}
