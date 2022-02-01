using Billingares.Api.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

var allowedOrigins = builder.Configuration["AllowedOrigins"].Split(',', StringSplitOptions.RemoveEmptyEntries);

builder.Services.AddCors(o => 
	o.AddPolicy("AllowAll", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
			   //.WithExposedHeaders("Access-Control-Allow-Origin", "Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
	}));

var app = builder.Build();

app.UseRouting();

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
	endpoints.MapGrpcService<ClaimsService>()
		.RequireCors("AllowAll");
});


app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
