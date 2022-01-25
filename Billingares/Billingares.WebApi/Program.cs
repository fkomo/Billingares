using Billingares.Base;
using Billingares.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddJsonConsole();

builder.Services.AddSingleton<ClaimsRepository>();

builder.Services.AddSingleton<ClaimsRepository>();

// client addresses needs to be allowed here
var AllowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: AllowSpecificOrigins,
					  builder =>
					  {
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

		return Results.Ok(repository.List(id));
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