//This file defines the data models used to deserialize the JSON response from the Flickr API.
//It contains several classes representing the structure of the response, the photos, and the selected photo.
//cusing the Newtonsoft.Json library for JSON serialization and deserialization.

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

        [JsonProperty("ownername")]
        public string Author { get; set; }
    }

    public class SelectedPhoto
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }

    public static class PhotoData
    {
        public static SelectedPhoto CurrentSelectedPhoto { get; set; }
    }
}

