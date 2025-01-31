using System;
namespace FlickrSearchApp.Services
{
	public class SettingsService
	{
		private const string ApiKey = "2c7633b7724f52403deb5c0192aa0e21";

        public void SaveApiKey(string apiKey)
        {
            Preferences.Set(ApiKey, apiKey);

        }

        public string GetApiKey()
        {
            return Preferences.Get(ApiKey, string.Empty); // Default is an empty string if not set
        }
    }

	
}

