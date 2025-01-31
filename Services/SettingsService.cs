using System;
namespace FlickrSearchApp.Services
{
	public class SettingsService
	{
        const string API_KEY_NAME = "flickr_api_key"; // Key name for storage
        const string DEFAULT_API_KEY = "2c7633b7724f52403deb5c0192aa0e21";

        public void SaveApiKey(string apiKey) =>
        Preferences.Set(API_KEY_NAME, apiKey);

        public string GetApiKey() =>
            Preferences.Get(API_KEY_NAME, DEFAULT_API_KEY);
    }

	
}

