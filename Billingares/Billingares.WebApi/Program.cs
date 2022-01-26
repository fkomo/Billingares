using Billingares.Base;
using Billingares.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Billingares.Api.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddJsonConsole();

builder.Services.AddSingleton<ClaimsRepository>();

var AllowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: AllowSpecificOrigins,
		builder =>
		{
			// client addresses needs to be allowed here
			builder.AllowAnyOrigin();
			builder.AllowAnyMethod();
			builder.AllowAnyHeader();
		});
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(AllowSpecificOrigins);


app.MapGet("/", () =>
{
	return "Billingares.Api";
});

app.MapPost("/api/transactions", (bool optimize, [FromBody] RequestEnvelope<Claim[]> request) =>
{
	try
	{
		if (request == null)
			return Results.BadRequest(nameof(request));

		if (string.IsNullOrWhiteSpace(request.ClientId))
			return Results.BadRequest(nameof(request.ClientId));

		if (request.Payload == null)
			return Results.BadRequest(nameof(request.Payload));

		var tBag = new TransactionBag();

		foreach (var claim in request.Payload)
			foreach (var transaction in claim.Transactions)
				tBag.Add(transaction);

		var transactions = optimize ?
			tBag.Minimalize().ToArray() :
			tBag.Transactions.ToArray();

		return Results.Ok(transactions);
	}
	catch (global::System.Exception)
	{
		return Results.Problem();
	}
});

app.MapGet("/api/claims/{id}", ([FromServices] ClaimsRepository repository, string id) =>
{
	try
	{
		if (string.IsNullOrWhiteSpace(id))
			return Results.BadRequest(nameof(id));

		var items = repository.List(id);

		return Results.Ok(items);
	}
	catch (global::System.Exception)
	{
		return Results.Problem();
	}
});

app.MapPost("/api/claim", ([FromServices] ClaimsRepository repository, [FromBody] RequestEnvelope<Claim> request) =>
{
	try
	{
		if (request == null)
			return Results.BadRequest(nameof(request));

		if (string.IsNullOrWhiteSpace(request.ClientId))
			return Results.BadRequest(nameof(request.ClientId));

		var claim = repository.Add(request.ClientId, request.Payload);

		return Results.Ok(claim);
	}
	catch (global::System.Exception)
	{
		return Results.Problem();
	}
});

app.MapPost("/api/claims", ([FromServices] ClaimsRepository repository, string id, [FromBody] RequestEnvelope<Claim[]> request) =>
{
	try
	{
		if (request == null)
			return Results.BadRequest(nameof(request));

		if (string.IsNullOrWhiteSpace(request.ClientId))
			return Results.BadRequest(nameof(request.ClientId));

		var claims = repository.Update(request.ClientId, request.Payload);

		return Results.Created($"/claims/{ request.ClientId }", claims.ToArray());
	}
	catch (global::System.Exception)
	{
		return Results.Problem();
	}
});

app.Run();