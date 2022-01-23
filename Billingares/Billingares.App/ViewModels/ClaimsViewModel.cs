using Billingares.Base;

namespace Billingares.App.ViewModels
{
	public class ClaimsViewModel : ViewModelBase<ClaimsViewModel>
	{
		public ClaimsViewModel() : base()
		{
			Claims = new List<Claim>();
			SearchString = string.Empty;

			ToAdd = new Claim();
		}

		public bool FilterFunc(Claim claim)
		{
			if (string.IsNullOrWhiteSpace(SearchString))
				return true;

			if (claim.Owner.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
				return true;

			if (claim.Description.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
				return true;

			if (claim.Against.Any(a => a.Contains(SearchString, StringComparison.OrdinalIgnoreCase)))
				return true;

			return false;
		}

		public bool IsValid { get; set; }

		public Claim ToAdd { get; set; }

		public string SearchString { get; set; }

		public List<Claim> Claims { get; set; }

		public Claim Selected { get; set; }

		public Claim BeforeEdit { get; set; }

		public int SelectedItemIndex { get; set; }
	}
}
