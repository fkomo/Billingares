using Billingares.Api.Interfaces;
using Billingares.App.ViewModels;
using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;

namespace Billingares.App.Components
{
	public partial class VersionComponent : ComponentBase<VersionViewModel, ApplicationState>
	{
		[Inject]
		private ApplicationSettings AppSettings { get; set; }

		protected async override Task OnLoadDataAsync()
		{
			ViewModel.ApiVersion = await ApiVersion();
			ViewModel.ClientVersion = $"{ AppSettings.Name } { AppSettings.Version }";
		}

		private async Task<string> ApiVersion()
		{
			IsBusy = true; 
			
			var response = await CreateClient().Version();

			IsBusy = false;

			return response;
		}

		private IInfoApi CreateClient()
		{
			if (string.IsNullOrWhiteSpace(AppSettings?.ApiUrl))
				return null;

			return AppSettings.ApiType switch
			{
				"REST" => new Api.Client.REST.InfoClient(AppSettings.ApiUrl),
				"gRPC" => new Api.Client.gRPC.InfoClient(AppSettings.ApiUrl),
				_ => throw new NotImplementedException($"{ nameof(AppSettings.ApiType) }:{ AppSettings.ApiType }"),
			};
		}
	}
}
