using Billingares.App.ViewModels;
using Billingares.Base;

namespace Billingares.App.Components
{
	public partial class ClaimsComponent : ComponentBase<ClaimsViewModel>
	{
		protected override async Task LoadDataAsync()
		{
			IsBusy = true;

			ViewModel.Claims = new List<Claim>();

			IsBusy = false;

			await Task.CompletedTask;
		}

		private void BackupItem(object element)
		{
			var claimToBackup = (Claim)element;
			ViewModel.BeforeEdit = new Claim(claimToBackup);
		}

		private void ItemHasBeenCommitted(object element)
		{

		}

		private void ResetItemToOriginalValues(object element)
		{
			var claim = (Claim)element;

			claim.Owner = ViewModel.BeforeEdit.Owner;
			claim.Amount = ViewModel.BeforeEdit.Amount;
			claim.AgainstList = ViewModel.BeforeEdit.AgainstList;
			claim.Description = ViewModel.BeforeEdit.Description;
		}

		private async Task AddNewClaim()
		{
			ViewModel.ToAdd.AgainstList = string.Join(", ", ViewModel.ToAdd.Against.Select(a => a.Trim()));

			ViewModel.Claims.Add(ViewModel.ToAdd);
			ViewModel.ToAdd = new Claim();

			await Task.CompletedTask;
		}
	}
}
