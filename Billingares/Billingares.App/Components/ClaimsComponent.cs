using Billingares.Api.Client.Services;
using Billingares.App.ViewModels;
using Billingares.Base;
using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;

namespace Billingares.App.Components
{
	public partial class ClaimsComponent : ComponentBase<ClaimsViewModel, ApplicationState>
	{
		[Inject]
		private ApplicationSettings AppSettings { get; set; }

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

		private async Task ListClaimsAsync()
		{
			IsBusy = true;

			var client = new ClaimsClient(AppSettings.ApiUrl);
			var response = await client.List(AppState.ClientId);

			ViewModel.Claims = response.ToList();

			IsBusy = false;
		}

		private async Task AddClaimAsync(Claim claim)
		{
			IsBusy = true;

			var client = new ClaimsClient(AppSettings.ApiUrl);
			var response = await client.Add(AppState.ClientId, claim);

			ViewModel.Claims.Add(response);

			IsBusy = false;
		}

		private async Task UpdateClaimsAsync()
		{
			IsBusy = true;

			var client = new ClaimsClient(AppSettings.ApiUrl);
			var response = await client.Update(AppState.ClientId, ViewModel.Claims.ToArray());

			ViewModel.Claims = response.ToList();

			IsBusy = false;
		}
	}
}
