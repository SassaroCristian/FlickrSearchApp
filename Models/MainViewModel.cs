using System.Collections.ObjectModel;
using System.ComponentModel;
using FlickrSearchApp.Services;
using System.Windows.Input;

namespace FlickrSearchApp.Models
{
	public class MainViewModel : INotifyPropertyChanged
	{
        private readonly FlickrService _flickrService;
        private ObservableCollection<Photo> _photos;

		private string _searchQuery;
		private bool _isLoading;
        private int _currentPage = 1;

        public MainViewModel(FlickrService flickrService)
        {
            _flickrService = flickrService;
            Photos = new ObservableCollection<Photo>();
            SearchCommand = new Command(async () => await ExecuteSearchCommand()); // SearchCommand initialization
            LoadMoreCommand = new Command(async () => await ExecuteLoadMoreCommand());
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
        public ICommand LoadMoreCommand { get; }

        private async Task ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;

            IsLoading = true;

            _currentPage = 1;
            Photos.Clear(); // Clear previous photos

            var result = await _flickrService.SearchPhotosAsync(SearchQuery);

            if (result != null && result.Photos?.PhotoList != null && result.Photos.PhotoList.Count > 0)
            {
                // Add the 5 photos to the collection
                foreach (var photo in result.Photos.PhotoList)
                {
                    Photos.Add(photo);
                }
            }

            IsLoading = false;
        }

        private async Task ExecuteLoadMoreCommand()
        {
            if (IsLoading) return; // Avoid multiple simultaneous loads

            IsLoading = true;
            _currentPage++;

            var result = await _flickrService.SearchPhotosAsync(SearchQuery, _currentPage);

            if (result != null && result.Photos?.PhotoList != null && result.Photos.PhotoList.Count > 0)
            {
                // Add the next set of photos to the collection
                foreach (var photo in result.Photos.PhotoList)
                {
                    Photos.Add(photo);
                }
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

