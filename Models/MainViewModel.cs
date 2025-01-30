using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlickrSearchApp.Models;
using FlickrSearchApp.Services;
using Microsoft.Maui.Controls;

namespace FlickrSearchApp.Models
{
	public class MainViewModel : INotifyPropertyChanged
	{
        private readonly FlickrService _flickrService;
        private ObservableCollection<Photo> _photos;

		private string _searchQuery;
		private bool _isLoading;

        public MainViewModel()
        {
            _flickrService = new FlickrService();
            Photos = new ObservableCollection<Photo>();
            SearchCommand = new Command(async () => await ExecuteSearchCommand()); // SearchCommand initialization
        }
		public string SearchQuery
		{
			get => _searchQuery;
			set
			{
                _searchQuery = value;
				OnPropertyChanged(nameof(SearchQuery)); // Notifies UI when loading state changes
            }
		}
        // Holds the search results retrieved from the Flickr API
        public ObservableCollection<Photo> Photos
        {
            get => _photos;
            set
            {
                if (_photos != value)
                {
                    _photos = value;
                    OnPropertyChanged(nameof(Photos)); 
                }
            }
        }

        // Indicates whether the app is currently fetching data
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading)); 
            }
        }

        public ICommand SearchCommand { get; }

        private async Task ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;

            IsLoading = true;

            Photos.Clear();

            // Add the new search results to the collection
            foreach (var photo in result.Photos.PhotoList)
            {
                Photos.Add(photo);
            }

            IsLoading = false;
        }
        
        // Event to notify the UI when a property changes
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

