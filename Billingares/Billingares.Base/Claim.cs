
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Billingares.Base
{
	public class Claim
	{
		[Required]
		public string Creditor { get; set; }

		[Required]
		[Range(1.0, double.MaxValue, ErrorMessage = "Amount must be greater than {1}.")]
		public decimal? Amount { get; set; }


		[Required]
		public string DebtorList 
		{
			get { return _debtorList; }
			set 
			{
				var fixedList = value?.Split(",", StringSplitOptions.RemoveEmptyEntries)?.Select(a => a.Trim());
				if (fixedList != null)
					_debtorList = string.Join(", ", fixedList);
				else
					_debtorList = null;
			}
		}
		private string _debtorList;

		public string Description { get; set; }

		public Claim()
		{

		}

		public Claim(string creditor, decimal amount, string debtorList)
		{
			Creditor = creditor;
			Amount = amount;
			DebtorList = debtorList;
		}

		public Claim(Claim claim)
		{
			Creditor = claim.Creditor;
			Amount = claim.Amount;
			_debtorList = claim._debtorList;
			Description = claim.Description;
		}

		[JsonIgnore]
		public string[] Debtors => DebtorList?.Split(", ")?.ToArray();

		[JsonIgnore]
		public IEnumerable<Transaction> Transactions
		{
			get
			{
				if (!Amount.HasValue)
					return null;

				return Debtors
					?.Except(new string[] { Creditor })
					?.Select(a => new Transaction(a, Creditor, Math.Round(Amount.Value / (decimal)Debtors.Length, 2)));
			}
		}
	}
}