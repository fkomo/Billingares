using Billingares.App;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Ujeby.Blazor.Base.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped<ClipboardService>();

builder.Services.AddScoped<ApplicationState>();

builder.Services.AddSingleton((_) =>
	builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>());


if (builder.Configuration["ApplicationSettings:ApiType"] == "gRPC")
{
	// option1
	builder.Services.AddSingleton(services =>
	{
		var httpClient = new HttpClient(
			new Grpc.Net.Client.Web.GrpcWebHandler(Grpc.Net.Client.Web.GrpcWebMode.GrpcWeb, new HttpClientHandler()));

		var channel = Grpc.Net.Client.GrpcChannel.ForAddress(builder.Configuration["ApplicationSettings:ApiUrl"],
			new Grpc.Net.Client.GrpcChannelOptions { HttpClient = httpClient });

		return new Billingares.Api.Interfaces.gRPC.Claims.ClaimsClient(channel);
	});

	// option2
	//builder.Services
	//    .AddGrpcClient<Billingares.Base.gRPC.Claims.ClaimsClient>(options =>
	//    {
	//        options.Address = new Uri(builder.Configuration["ApplicationSettings:ApiUrl"]);
	//    })
	//    .ConfigurePrimaryHttpMessageHandler(
	//        () => new Grpc.Net.Client.Web.GrpcWebHandler(new HttpClientHandler()));
}

await builder.Build().RunAsync();
