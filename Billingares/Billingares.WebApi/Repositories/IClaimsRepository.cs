using Billingares.Base;
using Ujeby.Api.Base.Repositories;

namespace Billingares.WebApi.Repositories
{
	public interface IClaimsRepository : IRepository<IEnumerable<Claim>>, IListRepository<Claim>
	{

	}
}
