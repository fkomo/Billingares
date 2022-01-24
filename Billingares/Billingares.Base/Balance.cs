
namespace Billingares.Base
{
	public class Balance
	{
		public string Name { get; set; }
		public decimal Amount { get; set; }

		public decimal AmountIn { get; set; }
		public decimal AmountOut { get; set; }

		public Balance(string name, decimal amount)
		{
			Name = name;
			Amount = amount;
		}

		public Balance(string name, decimal amountIn, decimal amountOut)
		{
			Name = name;
			Amount = amountIn - amountOut;
			AmountIn = amountIn;
			AmountOut = amountOut;
		}

		public override string ToString() => $"{ Name }={ Amount.ToString("0.00") }";
	}
}