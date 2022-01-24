using Billingares.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/claims", () =>
{
	return new Claim[]
	{
		new Claim("user0", 100.0M, "user1, user2, user3"),
		new Claim("user1", 50.0M, "user0, user2"),
	};
});

app.Run();