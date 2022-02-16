using Ujeby.Blazor.Base.ViewModels;

namespace Billingares.Blazor.ViewModels
{
	public class NotificationsViewModel : ViewModelBase
	{
		public List<string> Notifications { get; set; }

		public NotificationsViewModel() : base()
		{
			Notifications = new List<string>();
		}
	}
}
