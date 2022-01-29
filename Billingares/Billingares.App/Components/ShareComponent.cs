using Billingares.App.ViewModels;
using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;
using Ujeby.Blazor.Base.Services;

namespace Billingares.App.Components
{
	public partial class ShareComponent : ComponentBase<ShareViewModel, ApplicationState>
	{
		private bool IsSecureConnection =>
			new string[] { "https://", "http://localhost" }
				.Any(s => MyNavigationManager.BaseUri.StartsWith(s));

		[Inject] 
		private ClipboardService ClipboardService { get; set; }

		[Inject]
		public NavigationManager MyNavigationManager { get; set; }

		private async Task GenerateShareLinkAsync()
		{
			ViewModel.ShareUrl = $"{ MyNavigationManager.BaseUri }{ AppState.ClientId }";
			ViewModel.IsOpen = true;

			await Task.CompletedTask;
		}

		private async Task CopyUrlToClipboardAsync()
		{
			await ClipboardService.WriteTextAsync(ViewModel.ShareUrl);
			ViewModel.IsOpen = false;
		}
	}
}