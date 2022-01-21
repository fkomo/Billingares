
namespace Billingares.Base
{
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