using Billingares.Api.Interfaces;
using Billingares.Base;
using Billingares.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Ujeby.Api.Base;

namespace Billingares.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ClaimsController : ApiControllerBase, IClaimsApi
	{
		private IClaimsRepository Repository { get; set; }

		public ClaimsController([FromServices] IClaimsRepository repository)
		{
			Repository = repository;
		}

		[HttpPost]
		public Task<Claim> Add(string clientId, Claim claim)
		{
			return Task.FromResult(Repository.Add(clientId, claim));
		}

		[HttpGet]
		public Task<IEnumerable<Claim>> List(string clientId)
		{
			return Task.FromResult(Repository.List(clientId));
		}

		[HttpPost]
		public Task<IEnumerable<Claim>> Update(string clientId, Claim[] claims)
		{
			return Task.FromResult(Repository.Update(clientId, claims));
		}
	}
}
