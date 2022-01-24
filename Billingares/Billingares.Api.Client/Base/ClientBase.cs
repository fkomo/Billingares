using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Billingares.Api.Client.Base
{
	public class ClientBase
	{
		protected string BaseUrl { get; private set; }

		protected static HttpClient HttpClient { get; set; }

		static ClientBase()
		{
			HttpClient = new HttpClient();
		}

		public ClientBase(string baseUrl)
		{
			BaseUrl = baseUrl;
		}

		//protected Uri GetUri(string controllerRoute, string action, string queryString = null)
		//{
		//	// controller route must end with a slash
		//	if (!controllerRoute.EndsWith('/') && !controllerRoute.EndsWith('\\'))
		//		controllerRoute += '/';

		//	var baseUri = new Uri(BaseUrl);
		//	var endpointUri = new Uri(baseUri, controllerRoute);

		//	var actionUri = string.IsNullOrWhiteSpace(queryString)
		//		? new Uri(endpointUri, action)
		//		: new Uri(endpointUri, action + "?" + queryString);

		//	return actionUri;
		//}

		//protected async Task<TResponse> Post<TRequest, TResponse>(
		//	string controllerRoute, TRequest request, string queryString = null, [CallerMemberName] string action = null)
		//{
		//	var uri = GetUri(controllerRoute, action, queryString);
		//	var response = await ClientBase.HttpClient.PostAsJsonAsync(uri, request);

		//	response.EnsureSuccessStatusCode();

		//	return await Task.Run(async () => JsonConvert.DeserializeObject<TResponse>(
		//		await response.Content.ReadAsStringAsync()
		//	));
		//}

		//protected async Task<TResponse> Get<TResponse>(string controllerRoute, string queryString = null,
		//	[CallerMemberName] string action = null)
		//{
		//	var uri = GetUri(controllerRoute, action, queryString);
		//	var response = await ClientBase.HttpClient.GetAsync(uri);

		//	response.EnsureSuccessStatusCode();

		//	return await Task.Run(async () => JsonConvert.DeserializeObject<TResponse>(
		//		await response.Content.ReadAsStringAsync()
		//	));
		//}

		protected async Task<TResponse> Post<TRequest, TResponse>(string route, TRequest request)
		{
			var uri = new Uri(route);
			var response = await ClientBase.HttpClient.PostAsJsonAsync(uri, request);

			response.EnsureSuccessStatusCode();

			return await Task.Run(async () => JsonConvert.DeserializeObject<TResponse>(
				await response.Content.ReadAsStringAsync()
			));
		}

		protected async Task<TResponse> Get<TResponse>(string route)
		{
			var uri = new Uri(route);
			var response = await ClientBase.HttpClient.GetAsync(uri);

			response.EnsureSuccessStatusCode();

			return await Task.Run(async () => JsonConvert.DeserializeObject<TResponse>(
				await response.Content.ReadAsStringAsync()
			));
		}
	}
}