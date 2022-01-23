using Billingares.App.ViewModels;
using System.ComponentModel;

namespace Billingares.App.Components
{
	public class ComponentBase<TViewModel> : Microsoft.AspNetCore.Components.ComponentBase
		 where TViewModel : ViewModelBase<TViewModel>, new()
	{
		public TViewModel ViewModel { get; set; }

		public bool IsBusy { get; set; } = false;

		public ComponentBase()
		{
			ViewModel = new();
		}

		protected override async Task OnInitializedAsync()
		{
			await LoadDataAsync();
		}

		protected override async Task OnParametersSetAsync()
		{
			await LoadDataAsync();
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
            if (firstRender)
                ViewModel.PropertyChanged += ViewModelPropertyChanged;

            await base.OnAfterRenderAsync(firstRender);
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is TViewModel vm)
            {
                StateHasChanged();
            }
        }

		protected virtual Task LoadDataAsync()
		{
			return Task.CompletedTask;
		}
	}
}
