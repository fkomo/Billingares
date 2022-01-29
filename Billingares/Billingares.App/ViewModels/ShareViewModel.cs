using Ujeby.Blazor.Base.ViewModels;

namespace Billingares.App.ViewModels
{
	public class ShareViewModel : ViewModelBase
	{
		public bool IsOpen { get; set; }
		
		public string ShareUrl { get; set; }

		public ShareViewModel() : base()
		{
		}
	}
}
