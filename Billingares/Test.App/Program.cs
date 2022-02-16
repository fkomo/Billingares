using Billingares.Api.Client.gRPC;

try
{
	var client = new ClaimsClient("https://localhost:7063");
	var response = await client.List("test.app");

	foreach (var claim in response)
		Console.WriteLine(claim);
}
catch (Exception ex)
{
	Console.WriteLine(ex.ToString());
}
finally
{
	Console.ReadKey();
}

