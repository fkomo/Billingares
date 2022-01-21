
namespace Billingares.Base
{
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
}