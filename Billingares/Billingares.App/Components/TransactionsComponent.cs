using Billingares.App.ViewModels;
using Ujeby.Blazor.Base.Components;

namespace Billingares.App.Components
{
	public partial class TransactionsComponent : ComponentBase<TransactionsViewModel, ApplicationState>, IDisposable
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
			IsBusy = true;

			ViewModel.Transactions = AppState.Transactions;

			IsBusy = false;

			await Task.CompletedTask;
		}
	}
}
