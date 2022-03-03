using Ujeby.Blazor.Base.Components;
using Billingares.Blazor.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components;

namespace Billingares.Blazor.Components
{
	public partial class NotificationsComponent : ComponentBase<NotificationsViewModel, IBillingaresApplicationState, ApplicationSettings>, 
        IAsyncDisposable
	{
		private HubConnection HubConnection { get; set; }

        [Inject]
        MudBlazor.ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
		{
            if (!string.IsNullOrWhiteSpace(AppSettings.NotificationsUrl))
            {
                Snackbar.Configuration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomLeft;
                Snackbar.Configuration.NewestOnTop = false;
                Snackbar.Configuration.SnackbarVariant = MudBlazor.Variant.Filled;
                Snackbar.Configuration.PreventDuplicates = false;

                HubConnection = new HubConnectionBuilder()
                    .WithUrl(new Uri(AppSettings.NotificationsUrl))
                    .Build();

                HubConnection.On<string>("Notification", (message) =>
                {
                    ViewModel.Notifications.Add(message);

                    Snackbar.Add(message, 
                        MudBlazor.Severity.Info, 
                        config => 
                        { 
                            config.ShowCloseIcon = false;
                            config.ShowTransitionDuration = 500;
                            config.HideTransitionDuration = 500;
                            config.VisibleStateDuration = 5000;
                            config.CloseAfterNavigation = true;
                            config.HideIcon = false;
                        });

                    StateHasChanged();
                });

                await HubConnection.StartAsync();
            }
        }

		protected override async ValueTask DisposeAsyncCore()
		{
            if (HubConnection is not null)
            {
                await HubConnection.DisposeAsync();
            }
        }
    }
}
