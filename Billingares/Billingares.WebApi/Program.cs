using Billingares.Base;
using Billingares.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddJsonConsole();

builder.Services.AddSingleton<ClaimsRepository>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapGet("/", () =>
{
	return "Billingares.Api";
});

app.MapGet("/api/claims/{id}", ([FromServices] ClaimsRepository repository, string id) =>
{
	try
	{
		if (string.IsNullOrWhiteSpace(id))
			return Results.BadRequest(nameof(id));

		return Results.Ok(repository.List(id));
	}
	catch (global::System.Exception)
	{
		return Results.Problem();
	}
});

app.MapPost("/api/claim/{id}", ([FromServices] ClaimsRepository repository, string id, Claim item) =>
{
	try
	{
		if (string.IsNullOrWhiteSpace(id))
			return Results.BadRequest(nameof(id));

		item = repository.Add(id, item);

		return Results.Ok(item);
	}
	catch (global::System.Exception)
	{
		return Results.Problem();
	}
});

app.MapPost("/api/claims/{id}", ([FromServices] ClaimsRepository repository, string id, Claim[] items) =>
{
	try
	{
		if (string.IsNullOrWhiteSpace(id))
			return Results.BadRequest(nameof(id));

		return Results.Created($"/claims/{ id }", repository.Update(id, items));
	}
	catch (global::System.Exception)
	{
		return Results.Problem();
	}
});

app.Run();