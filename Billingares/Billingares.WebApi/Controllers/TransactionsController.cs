using Billingares.Api.Interfaces;
using Billingares.Base;
using Microsoft.AspNetCore.Mvc;
using Ujeby.Api.Base;

namespace Billingares.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TransactionsController : ApiControllerBase, ITransactionsApi
	{
		[HttpPost]
		public Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize)
		{
			return Task.FromResult(new TransactionBag().Add(claims, optimize));
		}
	}
}
