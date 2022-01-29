using Billingares.Base;
using Ujeby.Blazor.Base.ViewModels;

namespace Billingares.App.ViewModels
{
	public class ClaimsViewModel : ViewModelBase
	{
		public string SearchString { get; set; } = string.Empty;

		public List<Claim> Claims { get; set; } = new List<Claim>();

		public Claim ToAdd { get; set; } = new Claim();
		public Claim Selected { get; set; }

		public HashSet<Claim> SelectedItems { get; set; } = new HashSet<Claim>();

		public Claim BeforeEdit { get; set; }

		private bool optimize;
		public bool Optimize
		{
			get { return optimize; }
			set
			{
				if (value != optimize)
				{
					optimize = value;
					OnPropertyChanged();
				}
			}
		}

		public ClaimsViewModel() : base()
		{
		}

		public bool FilterFunc(Claim claim)
		{
			if (string.IsNullOrWhiteSpace(SearchString))
				return true;

			if (claim.Creditor.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
				return true;

			if (claim.Description.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
				return true;

			if (claim.Debtors.Any(a => a.Contains(SearchString, StringComparison.OrdinalIgnoreCase)))
				return true;

			return false;
		}
	}
}
