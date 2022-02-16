using Billingares.Api.Interfaces;
using Billingares.Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Ujeby.Api.Base;

namespace Billingares.Api.REST.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SessionsController : ApiControllerBase, ISessionsApi
	{
		private ISessionsRepository Repository { get; set; }

		public SessionsController([FromServices] ISessionsRepository repository)
		{
			Repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<Session>> List()
		{
			var result = await Repository.ListAsync();

			return result.Select(kd => 
				new Session 
				{ 
					Id = kd.Key, 
					LastChange = kd.Changed 
				});
		}
	}
}
