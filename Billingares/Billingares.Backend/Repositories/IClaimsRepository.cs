
using Ujeby.Api.Base.Repositories;

namespace Billingares.Backend.Repositories
{
	public interface IClaimsRepository : IRepository<IEnumerable<Claim>>, IListRepository<Claim>
	{

	}
}
