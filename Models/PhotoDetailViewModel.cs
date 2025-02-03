using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlickrSearchApp.Models
{
	public class PhotoDetailViewModel : INotifyPropertyChanged
	{
		private string _photoUrl;
		private string _title;
		private string _author;

		public event PropertyChangedEventHandler PropertyChanged;

        public string PhotoUrl
        {
            get => _photoUrl;
            set
            {
                _photoUrl = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public PhotoDetailViewModel(string photoUrl, string title, string author)
        {
            PhotoUrl = photoUrl;
            Title = title;
            Author = author;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

