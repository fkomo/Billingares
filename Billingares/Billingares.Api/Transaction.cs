
using System.Globalization;

namespace Billingares
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

		public override string ToString() => $"{ Flow }={ Amount:0.00}";

		public static string FormatCurrency(decimal c)
		{
			var cultureInfo = CultureInfo.GetCultureInfo("fr-FR");

			c = Math.Round(c, 1);
			if (c == 0 || (Math.Abs(c) >= 1 && (c / (int)c) == 1))
				return ((int)c).ToString("C0", cultureInfo);

			return c.ToString("C2", cultureInfo);
		}
	}

	public static class Extension
	{
		public static IEnumerable<string> ListUniqueUsers(this IEnumerable<Transaction> transactions)
		{
			return transactions.Select(t => t.From).Union(transactions.Select(t => t.To)).Distinct();
		}

		public static IEnumerable<string> ListUniqueUsersOrdered(this IEnumerable<Transaction> transactions)
		{
			return ListUniqueUsers(transactions).OrderBy(u => u);
		}
	}
}