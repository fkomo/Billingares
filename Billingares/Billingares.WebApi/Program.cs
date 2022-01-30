using Billingares.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Logging.AddJsonConsole();

// created once pre each injection
//builder.Services.AddTransient<IClaimsRepository, ClaimsRepository>();
// created once per request
builder.Services.AddScoped<IClaimsRepository, ClaimsRepository>();
// created only once per application
//builder.Services.AddSingleton<IClaimsRepository, ClaimsRepository>();

var allowedOrigins = builder.Configuration["AllowedOrigins"].Split(',', StringSplitOptions.RemoveEmptyEntries);

var AllowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: AllowSpecificOrigins,
		builder =>
		{
			//builder.AllowAnyOrigin();
			builder.WithOrigins(allowedOrigins);

			//builder.AllowAnyMethod();
			builder.WithMethods(
				"GET",
				"POST"
			);

			//builder.AllowAnyHeader();
			builder.WithHeaders(
				"content-type"
			);
		});
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(AllowSpecificOrigins);

app.UseAuthorization();
app.MapControllers();

app.Run();