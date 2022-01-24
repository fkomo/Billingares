using Billingares.App.ViewModels;
using Ujeby.Blazor.Base.Components;

namespace Billingares.App.Components
{
	public partial class TransactionsComponent : ComponentBase<TransactionsViewModel, ApplicationState>, IDisposable
	{
		protected override async Task OnInitializedAsync()
		{
			AppState.OnChange += OnTransactionsChanged;
			await base.OnInitializedAsync();
		}

		private async void OnTransactionsChanged()
		{
			await OnUpdateAsync();

			StateHasChanged();
		}

		protected override void Dispose(bool disposing)
		{
			AppState.OnChange -= OnTransactionsChanged;
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
