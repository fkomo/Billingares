
using Ujeby.Api.Base;

namespace Billingares.Backend.Repositories
{
	public interface ISessionsRepository
	{
		Task<IEnumerable<KeyDataChangedItem>> ListAsync();
	}
}
