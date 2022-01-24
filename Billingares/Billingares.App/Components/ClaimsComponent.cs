using Billingares.App.ViewModels;
using Billingares.Base;
using Ujeby.Blazor.Base.Components;

namespace Billingares.App.Components
{
	public partial class ClaimsComponent : ComponentBase<ClaimsViewModel, ApplicationState>
	{
		private async void OnItemUpdatedAsync(object element)
		{
			await OnUpdateAsync();
		}

		private void OnItemUpdatePreview(object element)
		{
			var claimToBackup = (Claim)element;
			ViewModel.BeforeEdit = new Claim(claimToBackup);
		}

		private void OnItemResetOriginal(object element)
		{
			var claim = (Claim)element;

			claim.Creditor = ViewModel.BeforeEdit.Creditor;
			claim.Amount = ViewModel.BeforeEdit.Amount;
			claim.DebtorList = ViewModel.BeforeEdit.DebtorList;
			claim.Description = ViewModel.BeforeEdit.Description;

			ViewModel.BeforeEdit = null;
		}

		private async Task AddNewItemAsync()
		{
			ViewModel.Claims.Add(ViewModel.ToAdd);
			ViewModel.ToAdd = new Claim();

			await OnUpdateAsync();
		}

		protected override async Task OnUpdateAsync()
		{
			var tBag = new TransactionBag();

			foreach (var claim in ViewModel.Claims)
				foreach (var transaction in claim.Transactions)
					tBag.Add(transaction);

			var transactions = ViewModel.OptimizedTransactions ?
				tBag.Minimalize().ToArray() : tBag.Transactions.ToArray();

			AppState.SetTransactions(transactions);

			await Task.CompletedTask;
		}

		public async Task RemoveSelectedItems()
		{
			foreach (var item in ViewModel.SelectedItems)
				ViewModel.Claims.Remove(item);

			ViewModel.SelectedItems.Clear();

			await OnUpdateAsync();
		}

		public async Task Save()
		{
			// TODO serialize claims and save somewhere ...
			await Task.CompletedTask;
		}
	}
}
