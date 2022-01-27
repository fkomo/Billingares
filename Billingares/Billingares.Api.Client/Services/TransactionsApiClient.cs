﻿using Billingares.Base;
using Ujeby.Api.Client.Base;

namespace Billingares.Api.Client.Services
{
	public class TransactionsApiClient : ClientApiBase, ITransactionsApi
	{
		public TransactionsApiClient(string baseUrl) : base(baseUrl)
		{
		}

		public Task<IEnumerable<Transaction>> List(string clientId, Claim[] claims, bool optimize)
		{
			return Post<Claim[], IEnumerable<Transaction>>
				($"transactions/list?{ nameof(clientId) }={ clientId }&{ nameof(optimize) }={ optimize }", claims);
		}
	}
}
