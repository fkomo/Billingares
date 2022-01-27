using Billingares.WebApi;
using Billingares.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

#if MINIMAL_API
#else
builder.Services.AddControllers();
#endif

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

#if MINIMAL_API
MinimalApi.Use(app);
#else
app.UseAuthorization();
app.MapControllers();
#endif

app.Run();