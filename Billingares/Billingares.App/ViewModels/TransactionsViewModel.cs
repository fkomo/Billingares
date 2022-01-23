using Billingares.Base;

namespace Billingares.App.ViewModels
{
	public class TransactionsViewModel : ViewModelBase<TransactionsViewModel>
	{
		public Transaction[] Transactions { get; set; }

		public bool Minimalized { get; set; }
	}
}
