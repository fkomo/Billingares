namespace Billingares
{
	public class AppInfo
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public string[] Endpoints { get; set; }

		public AppInfo()
		{

		}

		public AppInfo(string name, string version, string endpointBase, params (string, Type)[] endpoints)
		{
			Name = name;
			Version = version;

			Endpoints = endpoints.Select(e => 
				e.Item2.GetMembers().Select(mi => $"{ endpointBase }{ e.Item1 }/{ mi.Name.ToLower() }"))
					.SelectMany(e => e)
					.ToArray();
		}
	}
}
