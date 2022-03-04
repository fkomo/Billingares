using Billingares.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;
using Ujeby.Blazor.Base.Services;

namespace Billingares.Blazor.Components
{
	public partial class ShareComponent : ComponentBase<ShareViewModel, IBillingaresApplicationState, ApplicationSettings>
	{
		private bool IsSecureConnection =>
			new string[] { "https://", "http://localhost" }
				.Any(s => NavManager.BaseUri.StartsWith(s));

		[Inject] 
		private ClipboardService ClipboardService { get; set; }

		[Inject]
		public NavigationManager NavManager { get; set; }


		private async Task GenerateShareLinkAsync()
		{
			ViewModel.ShareUrl = $"{ NavManager.BaseUri }{ AppState.ClientId }";
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