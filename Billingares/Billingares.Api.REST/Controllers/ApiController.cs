using Billingares.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ujeby.Api.Base;

namespace Billingares.Api.REST.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ApiController : ApiControllerBase
	{
		private readonly IConfiguration Configuration;

		public ApiController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		[HttpGet]
		public Task<AppInfo> Index()
		{
			return Task.FromResult(new AppInfo(Configuration["AppInfo:Name"], Configuration["AppInfo:Version"], "/api",
				("/claims", typeof(IClaimsApi)), 
				("/transactions", typeof(ITransactionsApi))
			));
		}
	}
}
