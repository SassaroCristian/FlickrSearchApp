using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FlickrSearchApp.Models;


namespace FlickrSearchApp.Services
{
	public class FlickrService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;
        private readonly SettingsService _settingsService;

        // API base url for flickr
        private const string ApiUrl = "https://api.flickr.com/services/rest/";

		public FlickrService(SettingsService settingsService) 
		{
			_httpClient = new HttpClient();
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            // Get API key from Preference
            _apiKey = settingsService.GetApiKey();

        }

        // Method to search for photos based on a query and page number
        public async Task<FlickrResponse> SearchPhotosAsync(string query, int page = 1)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("API key is not set.");
            }

            // Construct the Api request url
            var url = $"{ApiUrl}?method=flickr.photos.search" +
                     $"&api_key={_apiKey}" +
                     $"&text={Uri.EscapeDataString(query)}" +
                     $"&page={page}" +
                     "&per_page=5" +  
                     "&extras=url_s,url_l" +
                     "&format=json&nojsoncallback=1";

            // Send the request and get the response as a string
            var response = await _httpClient.GetStringAsync(url);

            // Deserialize the JSON response into a FlickrResponse object
            var result = JsonConvert.DeserializeObject<FlickrResponse>(response);

            return result;
        }
    }
}

