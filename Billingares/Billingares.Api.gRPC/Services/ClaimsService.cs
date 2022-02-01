using Billingares.Api.Interfaces.gRPC;
using Grpc.Core;

namespace Billingares.Api.gRPC.Services
{
	public class ClaimsService : Claims.ClaimsBase
	{
		//private readonly ILogger<ClaimsService> _logger;
		//public ClaimsService(ILogger<ClaimsService> logger)
		//{
		//	_logger = logger;
		//}

		public override Task<ListResponse> List(ListRequest request, ServerCallContext context)
		{
			var response = new ListResponse();
			response.Claims.Add(
				new Claim()
				{
					Creditor = "test",
					Amount = 100.5f,
					Debtors = "test,test1,test2",
					Description = "description"
				});

			return Task.FromResult(response);
		}
	}
}