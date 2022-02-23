using Billingares.Backend;

try
{
	//var client = new Billingares.Api.Client.gRPC.ClaimsClient("https://localhost:7063");
	var client = new Billingares.Api.Client.REST.ClaimsClient("https://localhost:7103/api/");
	var response = await client.List("b3b4a01e5bd14819b286ab9ebf1337ad");

	foreach (var claim in response)
		Console.WriteLine(claim);

	var tb = new TransactionBag();

	foreach (var c in response)
		foreach (var t in c.Transactions)
			tb.Add(t);
}
catch (Exception ex)
{
	Console.WriteLine(ex.ToString());
}
finally
{
	Console.ReadKey();
}

