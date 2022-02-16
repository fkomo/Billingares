using Billingares.Api.REST.Hubs;
using Billingares.Backend.Repositories;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Logging.AddJsonConsole();

// created once pre each injection
//builder.Services.AddTransient<IClaimsRepository, ClaimsRepository>();
// created once per request
//builder.Services.AddScoped<IClaimsRepository, ClaimsRepository>();
// created only once per application
builder.Services.AddSingleton<IClaimsRepository, ClaimsRepository>();
builder.Services.AddSingleton<ISessionsRepository, SessionsRepository>();

var allowedOrigins = builder.Configuration["AllowedOrigins"].Split(',', StringSplitOptions.RemoveEmptyEntries);
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "AllowSpecificOrigins",
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
				"content-type",
				// SignalR
				"x-requested-with",
				"x-signalr-user-agent"
			);
		});
});

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
	opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
		new[] { "application/octet-stream" });
});


var app = builder.Build();

app.UseResponseCompression();

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();
app.MapControllers();

app.MapHub<ApiNotificationHub>("/api/notifications");

app.Run();