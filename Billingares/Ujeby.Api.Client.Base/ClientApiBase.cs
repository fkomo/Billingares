﻿using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Ujeby.Api.Client.Base
{
	public class RESTClientBase
	{
		protected Uri BaseUri { get; private set; }

		protected static HttpClient HttpClient { get; set; }

		static RESTClientBase()
		{
			HttpClient = new HttpClient();
		}

		public RESTClientBase(string baseUrl)
		{
			BaseUri = new Uri(baseUrl);
		}

		protected async Task<TResponse> Post<TRequest, TResponse>(string route, TRequest request)
		{
			var uri = new Uri(BaseUri, route);

			var response = await HttpClient.PostAsJsonAsync(uri, request);

			response.EnsureSuccessStatusCode();

			return await Task.Run(async () => JsonConvert.DeserializeObject<TResponse>(
				await response.Content.ReadAsStringAsync()
			));
		}

		protected async Task<TResponse> Get<TResponse>(string route = null)
		{
			var uri = new Uri(BaseUri, route);
			var response = await HttpClient.GetAsync(uri);

			response.EnsureSuccessStatusCode();

			return await Task.Run(async () => JsonConvert.DeserializeObject<TResponse>(
				await response.Content.ReadAsStringAsync()
			));
		}
	}
}