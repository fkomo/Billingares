using Billingares.Api.Interfaces;
using Billingares.Api.REST.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Ujeby.Api.Base;

namespace Billingares.Api.REST.Controllers
{
	[Route("api/[action]")]
	[ApiController]
	public class ApiController : ApiControllerBase
	{
		private readonly IConfiguration Configuration;

		private readonly IHubContext<ApiNotificationHub> HubContext;

		public ApiController(IConfiguration configuration, IHubContext<ApiNotificationHub> hubContext)
		{
			Configuration = configuration;
			HubContext = hubContext;
		}

		[HttpGet]
		[ActionName("")]
		public Task<AppInfo> Index()
		{
			return Task.FromResult(new AppInfo(Configuration["AppInfo:Name"], Configuration["AppInfo:Version"], "/api",
				("/claims", typeof(IClaimsApi)), 
				("/transactions", typeof(ITransactionsApi))
			));
		}

		[HttpPost]
		public async Task Notify(string message)
		{
			await HubContext.Clients.All.SendAsync("Notification", message);
		}
	}
}
