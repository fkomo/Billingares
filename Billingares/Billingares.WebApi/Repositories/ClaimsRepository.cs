using Billingares.Base;

namespace Billingares.WebApi.Repositories
{
	public interface IRepository<TItem>
		where TItem : class
	{
		TItem Get(string key);
		TItem Update(string key, TItem item);
	}

	public interface IListRepository<TItem>
		where TItem : class
	{
		TItem Add(string key, TItem item);
		IEnumerable<TItem> List(string key);
	}

	public interface IClaimsRepository : IRepository<IEnumerable<Claim>>, IListRepository<Claim>
	{

	}

	public class ClaimsRepository : IClaimsRepository
	{
		private readonly Dictionary<string, List<Claim>> Repository = new();

		public ClaimsRepository()
		{
		}

		public IEnumerable<Claim> Get(string key)
		{
			if (Repository.TryGetValue(key, out List<Claim> itemStorage))
				return itemStorage;

			var emptyList = Array.Empty<Claim>().ToList();
			Repository.Add(key, emptyList);
			return emptyList;
		}

		public Claim Add(string key, Claim item)
		{
			if (Repository.TryGetValue(key, out List<Claim> itemStorage))
				itemStorage.Add(item);

			else
				Repository.Add(key, new List<Claim>(new Claim[] { item }));

			return item;
		}

		public IEnumerable<Claim> Update(string key, IEnumerable<Claim> item)
		{
			if (Repository.Remove(key, out List<Claim> _))
			{
				Repository.Add(key, item.ToList());
				return item;
			}

			return Array.Empty<Claim>().ToList();
		}

		public IEnumerable<Claim> List(string key)
		{
			return Get(key);
		}
	}
}
