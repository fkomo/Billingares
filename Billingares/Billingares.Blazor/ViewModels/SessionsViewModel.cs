using Ujeby.Blazor.Base.ViewModels;

namespace Billingares.Blazor.ViewModels
{
	public class SessionsViewModel : ViewModelBase
	{
		public Session[] Sessions { get; set; } = Array.Empty<Session>();

		public SessionsViewModel() : base()
		{
		}
	}
}
