using Ujeby.Blazor.Base.ViewModels;

namespace Billingares.App.ViewModels
{
	public class VersionViewModel : ViewModelBase
	{
		private string apiVersion;
		public string ApiVersion
		{
			get { return apiVersion; }
			set
			{
				if (value != apiVersion)
				{
					apiVersion = value;
					OnPropertyChanged();
				}
			}
		}

		private string clientVersion;
		public string ClientVersion
		{
			get { return clientVersion; }
			set
			{
				if (value != clientVersion)
				{
					clientVersion = value;
					OnPropertyChanged();
				}
			}
		}

		public string Version => $"{ ClientVersion }, { ApiVersion }";

		public VersionViewModel() : base()
		{
		}
	}
}
