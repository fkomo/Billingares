using Billingares.Api.Interfaces;
using Billingares.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;

namespace Billingares.Blazor.Components
{
	public partial class SessionsComponent : ComponentBase<SessionsViewModel, IBillingaresApplicationState, ApplicationSettings>
	{
		protected async override Task OnLoadDataAsync()
		{
			var response = await ListSessionsAsync();
			ViewModel.Sessions = response.ToArray();
		}

		private async Task<IEnumerable<Session>> ListSessionsAsync()
		{
			IsBusy = true;

			var response = await CreateClient().List();

			IsBusy = false;

			return response;
		}

		[Inject]
		public NavigationManager MyNavigationManager { get; set; }

		public string BaseUri => MyNavigationManager.BaseUri;

		private ISessionsApi CreateClient()
		{
			return AppSettings.ApiType switch
			{
				"REST" => new Api.Client.REST.SessionsClient(AppSettings.ApiUrl),
				_ => throw new NotImplementedException($"{ nameof(AppSettings.ApiType) }:{ AppSettings.ApiType }"),
			};
		}
	}
}
