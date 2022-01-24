
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
		public string AgainstList 
		{
			get { return _againstList; }
			set 
			{
				var fixedList = value?.Split(",", StringSplitOptions.RemoveEmptyEntries)?.Select(a => a.Trim());
				if (fixedList != null)
					_againstList = string.Join(", ", fixedList);
				else
					_againstList = null;
			}
		}
		private string _againstList;

		public string[] Against => AgainstList?.Split(", ")?.ToArray();

		public string Description { get; set; }

		public Claim()
		{

		}

		public Claim(string owner, decimal amount, string againstList)
		{
			Owner = owner;
			Amount = amount;
			AgainstList = againstList;
		}

		public Claim(Claim claim)
		{
			Owner = claim.Owner;
			Amount = claim.Amount;
			_againstList = claim._againstList;
			Description = claim.Description;
		}

		public IEnumerable<Transaction> Transactions => Against
			?.Except(new string[] { Owner })
			?.Select(a => new Transaction(a, Owner, Math.Round(Amount / Against.Length, 2)));
	}
}