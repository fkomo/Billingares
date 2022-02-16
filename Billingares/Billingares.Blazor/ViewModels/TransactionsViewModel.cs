using Ujeby.Blazor.Base.ViewModels;

namespace Billingares.Blazor.ViewModels
{
	public class TransactionsViewModel : ViewModelBase
	{
		public Transaction[] Transactions { get; set; } = Array.Empty<Transaction>();

		public TransactionsViewModel() : base()
		{
		}

		public decimal?[] MatrixRow(string user)
		{
			return Transactions.ListUniqueUsersOrdered().Select(u =>
			{
				if (u == user)
					return decimal.MinValue;

				return Transactions.SingleOrDefault(t => t.From == user && t.To == u)?.Amount;
			}).ToArray();
		}

		public IEnumerable<Balance> Balance()
		{
			return Transactions.ListUniqueUsersOrdered().Select(u =>
			{
				var amountIn = Transactions.Where(t => t.To == u).Sum(t => t.Amount);
				var amountOut = Transactions.Where(t => t.From == u).Sum(t => t.Amount);
				
				return new Balance(u, amountIn, amountOut);
			}).ToArray();
		}
	}
}
