
using System.ComponentModel.DataAnnotations;

namespace Billingares.Base
{
	public class Claim
	{
		[Required]
		public string Owner { get; set; }

		[Required]
		[Range(1.0, double.MaxValue, ErrorMessage = "Amount must be greater than {1}.")]
		public decimal Amount { get; set; }

		[Required]
		public string AgainstList { get; set; }

		public string[] Against => AgainstList?.Split(",", StringSplitOptions.RemoveEmptyEntries);

		public string Description { get; set; }

		public Claim()
		{

		}

		public Claim(string owner, decimal amount, params string[] against)
		{
			Owner = owner;
			Amount = amount;
			AgainstList = string.Join(", ", against);
		}

		public Claim(Claim claim) : this(claim.Owner, claim.Amount, claim.Against)
		{
			Description = claim.Description;
		}

		public IEnumerable<Transaction> Transactions => Against
			.Except(new string[] { Owner })
			.Select(a => new Transaction(a, Owner, Math.Round(Amount / Against.Length, 1)));
	}
}