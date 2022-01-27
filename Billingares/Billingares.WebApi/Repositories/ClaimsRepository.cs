using Billingares.Base;

namespace Billingares.WebApi.Repositories
{
	public class ClaimsRepository
	{
		private readonly Dictionary<string, List<Claim>> Repository = new Dictionary<string, List<Claim>>();

		public ClaimsRepository()
		{
		}

		public IEnumerable<Claim> List(string id)
		{
			if (Repository.TryGetValue(id, out List<Claim> itemStorage))
				return itemStorage;

			var emptyList = Array.Empty<Claim>().ToList();
			Repository.Add(id, emptyList);
			return emptyList;
		}

		public Claim Add(string id, Claim item)
		{
			if (Repository.TryGetValue(id, out List<Claim> itemStorage))
				itemStorage.Add(item);

			else
				Repository.Add(id, new List<Claim>(new Claim[] { item }));

			return item;
		}

		public IEnumerable<Claim> Update(string id, Claim[] items)
		{
			if (Repository.Remove(id, out List<Claim> _))
			{
				Repository.Add(id, items.ToList());
				return items;
			}

			return Array.Empty<Claim>().ToList();
		}
	}
}
