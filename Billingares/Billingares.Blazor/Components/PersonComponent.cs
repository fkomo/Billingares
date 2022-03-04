using Billingares.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;

namespace Billingares.Blazor.Components
{
	public partial class PersonComponent : ComponentBase<PersonViewModel, IBillingaresApplicationState, ApplicationSettings>
	{
		[Parameter]
		public bool Disabled { get; set; } = false;

		[Parameter]
		public string Name { get; set; }

		protected override async Task OnInitializedAsync()
		{
			AppState.OnChange += OnAppStateChangedAsync;

			await base.OnInitializedAsync();
		}

		private async void OnAppStateChangedAsync()
		{
			await OnUpdateAsync();

			StateHasChanged();
		}

		protected override void Dispose(bool disposing)
		{
			AppState.OnChange -= OnAppStateChangedAsync;
		}

		protected override async Task OnUpdateAsync()
		{
			await Task.CompletedTask;
		}
	}
}
