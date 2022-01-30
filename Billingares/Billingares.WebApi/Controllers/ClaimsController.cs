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
		public async Task<Claim> Add(string clientId, Claim claim)
		{
			return await Repository.AddAsync(clientId, claim);
		}

		[HttpGet]
		public async Task<IEnumerable<Claim>> List(string clientId)
		{
			return await Repository.ListAsync(clientId);
		}

		[HttpPost]
		public async Task<IEnumerable<Claim>> Update(string clientId, Claim[] claims)
		{
			return await Repository.UpdateAsync(clientId, claims);
		}
	}
}
