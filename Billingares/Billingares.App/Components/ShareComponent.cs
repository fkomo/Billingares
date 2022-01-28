using Billingares.App.ViewModels;
using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;
using Ujeby.Blazor.Base.Services;

namespace Billingares.App.Components
{
	public partial class ShareComponent : ComponentBase<ShareViewModel, ApplicationState>
	{
		[Inject]
		private ApplicationSettings AppSettings { get; set; }

		[Inject]
		private MudBlazor.IDialogService DialogService { get; set; }

		[Inject] 
		private ClipboardService ClipboardService { get; set; }

		[Inject]
		public NavigationManager MyNavigationManager { get; set; }

		private async Task GenerateShareLinkAsync()
		{
			ViewModel.ShareUrl = $"{ MyNavigationManager.BaseUri }{ AppState.ClientId }";

			await DialogService.ShowMessageBox(ViewModel.ShareUrl, "", "Copy",
				options: new MudBlazor.DialogOptions
				{
					CloseButton = false,
					NoHeader = false,
				});

			await ClipboardService.WriteTextAsync(ViewModel.ShareUrl);

			StateHasChanged();
		}
	}
}