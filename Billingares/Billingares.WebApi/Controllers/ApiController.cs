using Billingares.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ujeby.Api.Base;

namespace Billingares.WebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ApiController : ApiControllerBase
	{
		[HttpGet]
		public Task<ApiInfo> Index()
		{
			return Task.FromResult(new ApiInfo("Billingares.Api", "/api",
				("/claims", typeof(IClaimsApi)), 
				("/transactions", typeof(ITransactionsApi))
			));
		}
	}
}
