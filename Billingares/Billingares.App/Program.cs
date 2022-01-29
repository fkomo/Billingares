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

await builder.Build().RunAsync();
