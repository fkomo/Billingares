using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billingares.Blazor
{
	public class Helpers
	{
		public static string GetChipStyle(string id)
		{
			return $"background-color:#{ id.GetHashCode():x};";
		}
	}
}
