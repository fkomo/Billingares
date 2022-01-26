namespace Billingares.Api.Client
{
	public class RequestEnvelope<TPayload>
	{
		public string ClientId { get; private set; }

		public TPayload Payload { get; private set; }

		public RequestEnvelope(string clientId, TPayload payload)
		{
			ClientId = clientId;
			Payload = payload;
		}
	}
}