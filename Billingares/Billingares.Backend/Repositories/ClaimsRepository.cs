using Billingares.Base;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Ujeby.Api.Base.Db;

namespace Billingares.Backend.Repositories
{
	public class ClaimsRepository : KeyDataRepository, IClaimsRepository
	{
		public ClaimsRepository(IConfiguration configuration) : base(null)
		{
			ConnectionString = configuration["ConnectionStrings:mysql-ujeby"];
		}

		public async Task<IEnumerable<Claim>> GetAsync(string key)
		{
			var result = await this.ReadAsync(
				new KeyDataItem
				{
					Key = key,
				});

			if (result == null)
				return Array.Empty<Claim>();

			return Deserialize(result.Data);
		}

		public async Task<IEnumerable<Claim>> UpdateAsync(string key, IEnumerable<Claim> item)
		{
			var result = await this.ReadAsync(
				new KeyDataItem
				{
					Key = key
				});

			var keyItem = new KeyDataItem
			{
				Key = key,
				Data = Serialize(item)
			};

			if (result == null)
				result = await this.CreateAsync(keyItem);

			else
				result = await this.UpdateAsync(keyItem);

			return Deserialize(result.Data);
		}

		public async Task<Claim> AddAsync(string key, Claim item)
		{
			var current = await GetAsync(key);

			var list = current.ToList();
			list.Add(item);

			await UpdateAsync(key, list);
			return item;
		}

		public async Task<IEnumerable<Claim>> ListAsync(string key)
		{
			return await GetAsync(key);
		}

		private string Serialize(IEnumerable<Claim> data)
		{
			return JsonSerializer.Serialize(data);
		}

		private IEnumerable<Claim> Deserialize(string data)
		{
			return JsonSerializer.Deserialize<Claim[]>(data);
		}
	}
}
