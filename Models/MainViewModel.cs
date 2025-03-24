//This file defines the ViewModel for the MainPage in the application.
//The MainViewModel acts as the intermediary between the user interface (UI) and the underlying business logic (in this case, interacting with the Flickr API).
//It implements the INotifyPropertyChanged interface to notify the UI about changes to properties.

using System.Collections.ObjectModel;
using System.ComponentModel;
using FlickrSearchApp.Services;
using System.Windows.Input;

namespace FlickrSearchApp.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // instance to fetch photos 
        private readonly FlickrService _flickrService;

        // Holds the retrived photos
        private ObservableCollection<Photo> _photos;

        // fields with property change notifications
        private string _searchQuery;
        private bool _isLoading;
        private int _currentPage = 1; 

        // Store the selected photo for navigation to the PhotoDetailPage
        private Photo _selectedPhoto;

        // Contructor to initialize the ViewModel, sets commands
        public MainViewModel(FlickrService flickrService)
        {
           
            _flickrService = flickrService;
           
            Photos = new ObservableCollection<Photo>();

            // Commands bound to the ui 
            SearchCommand = new Command(async () => await ExecuteSearchCommand());
            LoadMoreCommand = new Command(async () => await ExecuteLoadMoreCommand());
            PhotoClickCommand = new Command<Photo>(async (photo) => await OnPhotoClick(photo));
        }

        // Two-way data binding for search input
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery)); // notify ui of changes 
            }
        }

        // ObservableCollection automatically notifies UI of changes
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

        // Loading state for the spinner
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        // Commands avaiable in the ui 
        public ICommand SearchCommand { get; }
        public ICommand LoadMoreCommand { get; }
        public ICommand PhotoClickCommand { get; }


        private async Task ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;

            IsLoading = true;
            _currentPage = 1;
            Photos.Clear(); // Reset collection for new search

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
                    Photos.Add(photo); // adds to the existing collection
                }
            }

            IsLoading = false;
        }

        private async Task OnPhotoClick(Photo clickedPhoto)
        {
            if (clickedPhoto == null)
                return;

            SelectedPhoto selectedPhoto = new SelectedPhoto
            {
                Url = clickedPhoto.UrlLarge, // Full size image URL
                Title = clickedPhoto.Title,  // Title of the photo
                Author = clickedPhoto.Author // Author of the photo
            };

            // Store the selected photo in the static PhotoData class
            PhotoData.CurrentSelectedPhoto = selectedPhoto;
            // Navigate to the PhotoDetailPage
            await Shell.Current.GoToAsync("//PhotoDetailPage");
        }

        // Event to notify the UI when a property changes
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Provide access to the selected photo for PhotoDetailPage binding
        public Photo SelectedPhoto => _selectedPhoto;
    }
}