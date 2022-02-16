
using Microsoft.Extensions.Configuration;
using Ujeby.Api.Base;
using Ujeby.Api.Base.Db;

namespace Billingares.Backend.Repositories
{
	public class SessionsRepository : KeyDataRepository, ISessionsRepository
	{
		public SessionsRepository(IConfiguration configuration) : base(null)
		{
			ConnectionString = configuration["ConnectionStrings:mysql-ujeby"];
		}

		public async new Task<IEnumerable<KeyDataChangedItem>> ListAsync()
		{
			return await base.ListAsync();
		}
	}
}
