using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Billingares.Api.Client
{
	public class ClientBase
	{
		protected Uri BaseUri { get; private set; }

		protected static HttpClient HttpClient { get; set; }

		static ClientBase()
		{
			HttpClient = new HttpClient();
		}

		public ClientBase(string baseUrl)
		{
			BaseUri = new Uri(baseUrl);
		}

		protected async Task<TResponse> Post<TRequest, TResponse>(string route, TRequest request)
		{
			var uri = new Uri(BaseUri, route);

			var response = await ClientBase.HttpClient.PostAsJsonAsync(uri, request);

			response.EnsureSuccessStatusCode();

			return await Task.Run(async () => JsonConvert.DeserializeObject<TResponse>(
				await response.Content.ReadAsStringAsync()
			));
		}

		protected async Task<TResponse> Get<TResponse>(string route)
		{
			var uri = new Uri(BaseUri, route);
			var response = await ClientBase.HttpClient.GetAsync(uri);

			response.EnsureSuccessStatusCode();

			return await Task.Run(async () => JsonConvert.DeserializeObject<TResponse>(
				await response.Content.ReadAsStringAsync()
			));
		}
	}
}