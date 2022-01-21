
namespace Billingares.Base
{
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
}