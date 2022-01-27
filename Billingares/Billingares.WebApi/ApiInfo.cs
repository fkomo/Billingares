using System;

namespace Billingares.WebApi
{
	public class ApiInfo
	{
		public string Name { get; set; }
		public string[] Endpoints { get; set; }

		public ApiInfo(string name)
		{
			Name = name;
			Endpoints = Array.Empty<string>();
		}

		public ApiInfo(string name, string endpointBase, params (string, Type)[] endpoints) : this(name)
		{
			Name = name;
			Endpoints = endpoints.Select(e => 
				e.Item2.GetMembers().Select(mi => $"{ endpointBase }{ e.Item1 }/{ mi.Name.ToLower() }"))
					.SelectMany(e => e)
					.ToArray();
		}
	}
}
