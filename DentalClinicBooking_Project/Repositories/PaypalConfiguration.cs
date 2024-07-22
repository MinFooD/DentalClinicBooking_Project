using PayPal.Api;

namespace DentalClinicBooking_Project.Repositories
{
	public static class PaypalConfiguration
	{
		// Variables for storing the clientID and clientSecret key  
		public static readonly string ClientId;
		public static readonly string ClientSecret;

		static PaypalConfiguration()
		{
			// Get the configuration
			var config = GetConfig();
			ClientId = config["clientId"];
			ClientSecret = config["clientSecret"];
		}

		// Get configuration from appsettings.json
		public static Dictionary<string, string> GetConfig()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");

			IConfiguration configuration = builder.Build();

			var paypalConfig = new Dictionary<string, string>
			{
				{ "mode", configuration["PayPal:mode"] },
				{ "connectionTimeout", configuration["PayPal:connectionTimeout"] },
				{ "requestRetries", configuration["PayPal:requestRetries"] },
				{ "clientId", configuration["PayPal:clientId"] },
				{ "clientSecret", configuration["PayPal:clientSecret"] }
			};
			return paypalConfig;
		}
		private static string GetAccessToken()
		{
			// Get access token from PayPal  
			string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
			return accessToken;
		}
		public static APIContext GetAPIContext()
		{
			// Return APIContext object by invoking it with the access token  
			APIContext apiContext = new APIContext(GetAccessToken())
			{
				Config = GetConfig()
			};
			return apiContext;
		}

	}
}
