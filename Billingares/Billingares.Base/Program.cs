
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
			From = flow.Split(":")[0];
			To = flow.Split(":")[1];
			Amount = amount;
		}

		public string ToString(int id)
		{
			return string.Format(RowFormat, id, From, To, Amount.ToString("0.00"));
		}

		public const string RowFormat = " {0,5} | {1,-10} | {2,-10} | {3,10} ";
	}

	public class Claim
	{
		public string Payer { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public string[] Debtors { get; set; }

		public Claim(string payer, decimal amount, params string[] debtors)
		{
			Amount = amount;
			Payer = payer;
			Debtors = debtors;
		}
	}

	public class Program
	{
		public static string[] Users = new string[]
		{
			"aaa",
			"mmm",
			"nnn",
			"kkk",
			"fff",
			"rrr",
			"ppp",
		};

		public static List<Claim> Claims = new List<Claim>();

		public static void Main()
		{
			// input data
			Claims.Add(new Claim("aaa", 38.0M, Users.ToArray()));
			Claims.Add(new Claim("mmm", 20.0M, Users.ToArray()));
			Claims.Add(new Claim("ppp", 75.0M, Users.ToArray()));
			Claims.Add(new Claim("mmm", 36.0M, "aaa", "mmm", "kkk", "nnn", "ppp"));
			Claims.Add(new Claim("kkk", 90.0M, "aaa", "mmm", "kkk", "nnn", "ppp"));
			Claims.Add(new Claim("nnn", 27.0M, "fff", "rrr", "kkk", "nnn"));

			var transactions = new List<Transaction>();
			foreach (var claim in Claims)
				transactions.AddRange(
					claim.Debtors.Except(new string[] { claim.Payer })
						.Select(d =>
						{
							return new Transaction(d, claim.Payer, claim.Amount / claim.Debtors.Length);
						}));

			transactions = transactions.GroupBy(t => t.Flow)
				.Select(tg => new Transaction(tg.Key, tg.Sum(t => t.Amount)))
				.ToList();

			PrintTransactions(transactions.ToArray());
		}

		private static void PrintTransactions(Transaction[] transactions)
		{
			Console.WriteLine(string.Format(Transaction.RowFormat, "#", "From", "To", "Amount [e]"));
			Console.WriteLine("----------------------------------------------");

			var ordered = transactions.OrderBy(t => t.From).ToArray();
			for (var i = 0; i < ordered.Length; i++)
				Console.WriteLine(ordered[i].ToString(i + 1));
		}
	}
}