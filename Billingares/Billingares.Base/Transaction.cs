
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
}