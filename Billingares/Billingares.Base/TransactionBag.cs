
namespace Billingares.Base
{
	public class TransactionBag
	{
		public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

		public Transaction Add(Transaction transaction)
		{
			var oldTransaction = Transactions.SingleOrDefault(t => t.Flow == transaction.Flow);
			if (oldTransaction != null)
			{
				oldTransaction.Amount += transaction.Amount;
				return oldTransaction;
			}

			var reverseTransaction = Transactions.SingleOrDefault(t => t.ReverseFlow == transaction.Flow);
			if (reverseTransaction != null)
			{
				reverseTransaction.Amount = Math.Abs(transaction.Amount - reverseTransaction.Amount);

				if (transaction.Amount > reverseTransaction.Amount)
				{
					reverseTransaction.From = transaction.From;
					reverseTransaction.To = transaction.To;
				}

				return reverseTransaction;
			}

			Transactions.Add(transaction);
			return transaction;
		}

		public void Clear()
		{
			Transactions.Clear();
		}

		public IEnumerable<Transaction> Filter(decimal minAmount) => Transactions.Where(t => Math.Abs(t.Amount) > minAmount);

		private static IEnumerable<string> ListUsers(IEnumerable<Transaction> transactions) =>
			transactions.Select(t => t.From).Union(transactions.Select(t => t.To))
				.Distinct();

		public IEnumerable<Transaction> Minimalize()
		{
			var users = ListUsers(Transactions);

			var balance = new List<Balance>();
			foreach (var user in users)
			{
				var amount = Transactions.Where(t => t.From == user).Sum(t => t.Amount) -
					Transactions.Where(t => t.To == user).Sum(t => t.Amount);

				balance.Add(new Balance(user, amount));
			}

			var transactions = new List<Transaction>();
			while (balance.Any(b => b.Amount > 0))
			{
				var ordered = balance.OrderByDescending(b => b.Amount);

				var from = ordered.First();
				var to = ordered.Last();
				transactions.Add(new Transaction(from.Name, to.Name, from.Amount));

				to.Amount += from.Amount;
				from.Amount = 0;
			}

			return transactions;
		}

		public static string[] MatrixCsv(IEnumerable<Transaction> transactions)
		{
			var orderedUsers = ListUsers(transactions).OrderBy(t => t);

			var lines = new List<string>
			{
				";" + string.Join(';', orderedUsers)
			};

			foreach (var user in orderedUsers)
			{
				var amounts = orderedUsers.Select(u =>
				{
					return transactions.SingleOrDefault(t => t.From == user && t.To == u)?.Amount;
				});

				lines.Add(user + ";" + string.Join(';', amounts));
			}

			return lines.ToArray();
		}
	}
}
