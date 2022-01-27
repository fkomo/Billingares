﻿using Billingares.Api.Client.Services;
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

		private void OnItemEditPreview(object element)
		{
			var claimToBackup = (Claim)element;
			ViewModel.BeforeEdit = new Claim(claimToBackup);
		}

		private void OnItemEditCancel(object element)
		{
			var claim = (Claim)element;

			claim.Creditor = ViewModel.BeforeEdit.Creditor;
			claim.Amount = ViewModel.BeforeEdit.Amount;
			claim.DebtorList = ViewModel.BeforeEdit.DebtorList;
			claim.Description = ViewModel.BeforeEdit.Description;

			ViewModel.BeforeEdit = null;
		}

		private async void OnItemEditCommitAsync(object element)
		{
			//var changedClaim = element as Claim;
			var claims = await UpdateClaimsAsync(ViewModel.Claims.ToArray());

			ViewModel.Claims = claims.ToList();

			await OnUpdateAsync();
		}

		private async void OnItemSubmitAsync()
		{
			var toAddTmp = ViewModel.ToAdd;

			toAddTmp = await AddClaimAsync(toAddTmp);

			ViewModel.Claims.Add(toAddTmp);
			ViewModel.ToAdd = new Claim();

			await OnUpdateAsync();
		}

		private async void RemoveSelectedItemsAsync()
		{
			var newClaimsTmp = ViewModel.Claims.Except(ViewModel.SelectedItems);

			var claims = await UpdateClaimsAsync(newClaimsTmp.ToArray());

			ViewModel.Claims = claims.ToList();
			ViewModel.SelectedItems.Clear();

			await OnUpdateAsync();
		}

		protected override async Task OnLoadDataAsync()
		{
			var claims = await ListClaimsAsync();

			ViewModel.Claims = claims.ToList();

			await base.OnLoadDataAsync();
		}

		protected override async Task OnUpdateAsync()
		{
			var transactions = await ListTransactionsAsync(ViewModel.Claims.ToArray(), ViewModel.OptimizedTransactions);

			AppState.SetTransactions(transactions.ToArray());

			await base.OnUpdateAsync();
		}

		#region api calls

		private async Task<IEnumerable<Claim>> ListClaimsAsync()
		{
			IsBusy = true;

			var response = await CreateClaimsClient().List(AppState.ClientId);

			IsBusy = false;

			return response;
		}

		private async Task<Claim> AddClaimAsync(Claim claim)
		{
			IsBusy = true;

			var response = await CreateClaimsClient().Add(AppState.ClientId, claim);

			IsBusy = false;

			return response;
		}

		private async Task<IEnumerable<Claim>> UpdateClaimsAsync(Claim[] claims)
		{
			IsBusy = true;

			var response = await CreateClaimsClient().Update(AppState.ClientId, claims);

			IsBusy = false;

			return response;
		}

		private async Task<IEnumerable<Transaction>> ListTransactionsAsync(Claim[] claims, bool optimize)
		{
			IsBusy = true;

			var response = await CreateTransactionsClient().List(AppState.ClientId, claims, optimize);

			IsBusy = false;

			return response;
		}

		#endregion

		private IClaimsApi CreateClaimsClient()
		{
			if (!string.IsNullOrWhiteSpace(AppSettings.ApiUrl))
				return new ClaimsApiClient(AppSettings.ApiUrl);

			return new OfflineClaimsClient();
		}

		private ITransactionsApi CreateTransactionsClient()
		{
			if (!string.IsNullOrWhiteSpace(AppSettings.ApiUrl))
				return new TransactionsApiClient(AppSettings.ApiUrl);

			return new OfflineTransactionsClient();
		}
	}
}
