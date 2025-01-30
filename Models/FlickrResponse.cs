using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FlickrSearchApp.Models
{
	public class FlickrResponse
	{
		[JsonProperty("photos")]
		public Photos Photos { get; set; }
	}

    public class Photos
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pages")]
        public int TotalPages { get; set; }

        [JsonProperty("perpage")]
        public int PhotosPerPage { get; set; }

        [JsonProperty("total")]
        public int TotalPhotos { get; set; }

        [JsonProperty("photo")]
        public List<Photo> PhotoList { get; set; }
    }

    public class Photo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url_s")]
        public string UrlSmall { get; set; }

        [JsonProperty("url_l")]
        public string UrlLarge { get; set; }
    }
}

