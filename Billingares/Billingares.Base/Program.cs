
namespace Billingares.Base
{
	public class Transaction
	{
		public decimal Amount { get; set; }
		public string From { get; set; }
		public string To { get; set; }

		public string Flow => $"{ From }:{ To }";
		public string ReverseFlow => $"{ To }:{ From }";

		public Transaction(string from, string to, decimal amount)
		{
			From = from;
			To = to;
			Amount = amount;
		}

		public Transaction(string flow, decimal amount)
		{
			var split = flow.Split(":");
			From = split[0];
			To = split[1];

			Amount = amount;
		}

		public override string ToString() => $"{ Flow }={ Amount.ToString("0.00") }";
	}

	public class Balance
	{
		public string Name { get; set; }
		public decimal Amount { get; set; }

		public Balance(string name, decimal amount)
		{
			Name = name;
			Amount = amount;
		}

		public override string ToString()
		{
			return $"{ Name }={ Amount.ToString("0.00") }";
		}
	}

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

	public class Claim
	{
		public string Owner { get; set; }
		public decimal Amount { get; set; }
		public string[] Against { get; set; }

		public string Description { get; set; }

		public Claim(string owner, decimal amount, params string[] against)
		{
			Owner = owner;
			Against = against;
			Amount = amount;
		}

		public IEnumerable<Transaction> Transactions => Against
			.Except(new string[] { Owner })
			.Select(a => new Transaction(a, Owner, Math.Round(Amount / Against.Length, 1)));
	}

	public class Program
	{
		public static void Main()
		{
			var claims = new Claim[]
			{
				new Claim("jozo", 100.0M, "jozo", "fero", "tono", "jano"),
				new Claim("fero", 50.0M, "jozo", "tono"),
			};

			var tBag = new TransactionBag();

			foreach (var claim in claims)
				foreach (var transaction in claim.Transactions)
					tBag.Add(transaction);

			PrintTransactions("Simplified", tBag.Transactions);

			var minTransactionAmount = 1.0M;
			var filtered = tBag.Filter(minTransactionAmount);
			PrintTransactions($"Simplified & >{ minTransactionAmount }e", filtered);

			var minimalized = tBag.Minimalize();
			PrintTransactions($"Minimalized", minimalized);
		}

		private static void PrintTransactions(string title, IEnumerable<Transaction> transactions)
		{
			var rowFormat = " {0,3} | {1,-10} | {2,-10} | {3,10} ";

			var ordered = transactions.OrderBy(t => t.From).ToArray();
	
			Console.WriteLine($" { title } ");
			Console.WriteLine("--------------------------------------------");
			Console.WriteLine(string.Format(rowFormat, "#", "From", "To", "Amount [e]"));
			Console.WriteLine("--------------------------------------------");

			for (var i = 0; i < ordered.Length; i++)
				Console.WriteLine(string.Format(rowFormat, i + 1, ordered[i].From, ordered[i].To, ordered[i].Amount.ToString("0.00")));

			Console.WriteLine(Environment.NewLine);

			foreach (var line in TransactionBag.MatrixCsv(transactions))
				Console.WriteLine(line);
			Console.WriteLine(Environment.NewLine);
		}
	}
}