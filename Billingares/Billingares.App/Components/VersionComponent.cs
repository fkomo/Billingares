using Billingares.Api.Client.Services;
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
			if (string.IsNullOrWhiteSpace(AppSettings?.ApiUrl))
				return null;

			return await new ApiClient(AppSettings.ApiUrl).Version();
		}
	}
}
