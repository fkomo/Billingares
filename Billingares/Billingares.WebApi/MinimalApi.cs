using Billingares.Api.Interfaces;
using Billingares.Base;
using Billingares.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Billingares.WebApi
{
	public class MinimalApi
	{
		private static string ApiBaseUrl = "/api";
	
		public static void Use(WebApplication app)
		{
			app.MapGet(ApiBaseUrl, () =>
			{
				return new ApiInfo("Billingares.Api (minimal)", ApiBaseUrl,
					("/claims", typeof(IClaimsApi)),
					("/transactions", typeof(ITransactionsApi))
				);
			});

			app.MapPost($"{ ApiBaseUrl }/transactions/list", (bool optimize, string clientId, 
				[FromBody] Claim[] claims) =>
			{
				try
				{
					if (string.IsNullOrWhiteSpace(clientId))
						return Results.BadRequest(nameof(clientId));

					if (claims == null)
						return Results.BadRequest(nameof(claims));

					var transactions = new TransactionBag().Add(claims, optimize);

					return Results.Ok(transactions);
				}
				catch (global::System.Exception)
				{
					return Results.Problem();
				}
			});

			app.MapGet($"{ ApiBaseUrl }/claims/list", ([FromServices] IClaimsRepository repository, string clientId) =>
			{
				try
				{
					if (string.IsNullOrWhiteSpace(clientId))
						return Results.BadRequest(nameof(clientId));

					var items = repository.List(clientId);

					return Results.Ok(items);
				}
				catch (global::System.Exception)
				{
					return Results.Problem();
				}
			});

			app.MapPost($"{ ApiBaseUrl }/claims/add", ([FromServices] IClaimsRepository repository, string clientId, 
				[FromBody] Claim claim) =>
			{
				try
				{
					if (string.IsNullOrWhiteSpace(clientId))
						return Results.BadRequest(nameof(clientId));

					claim = repository.Add(clientId, claim);

					return Results.Ok(claim);
				}
				catch (global::System.Exception)
				{
					return Results.Problem();
				}
			});

			app.MapPost($"{ ApiBaseUrl }/claims/update", ([FromServices] IClaimsRepository repository, string clientId, 
				[FromBody] Claim[] claims) =>
			{
				try
				{
					if (string.IsNullOrWhiteSpace(clientId))
						return Results.BadRequest(nameof(clientId));

					var claimsUpdated = repository.Update(clientId, claims);

					return Results.Ok(claimsUpdated.ToArray());
				}
				catch (global::System.Exception)
				{
					return Results.Problem();
				}
			});
		}
	}
}
