using Billingares.Api.Interfaces;
using Billingares.Blazor.ViewModels;
using Ujeby.Blazor.Base.Components;

namespace Billingares.Blazor.Components
{
	public partial class TransactionsComponent : ComponentBase<TransactionsViewModel, ApplicationState, ApplicationSettings>, IDisposable
	{
		protected override async Task OnInitializedAsync()
		{
			AppState.OnChange += OnTransactionsChangedAsync;

			await base.OnInitializedAsync();
		}

		private async void OnTransactionsChangedAsync()
		{
			await OnUpdateAsync();

			StateHasChanged();
		}

		protected override void Dispose(bool disposing)
		{
			AppState.OnChange -= OnTransactionsChangedAsync;
		}

		protected override async Task OnUpdateAsync()
		{
			var response = await ListTransactionsAsync(AppState.Claims, AppState.OptimizeTransactions);

			ViewModel.Transactions = response.ToArray();
		}

		private async Task<IEnumerable<Transaction>> ListTransactionsAsync(Claim[] claims, bool optimize)
		{
			IsBusy = true;

			var response = await CreateClient().List(AppState.ClientId, claims, optimize);

			IsBusy = false;

			return response;
		}

		private ITransactionsApi CreateClient()
		{
			return AppSettings.ApiType switch
			{
				"REST" => new Api.Client.REST.TransactionsClient(AppSettings.ApiUrl),
				"gRPC" => new Api.Client.gRPC.TransactionsClient(AppSettings.ApiUrl),
				_ => throw new NotImplementedException($"{ nameof(AppSettings.ApiType) }:{ AppSettings.ApiType }"),
			};
		}
	}
}
