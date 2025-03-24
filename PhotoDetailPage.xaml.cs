//The PhotoDetailPage.xaml.cs file contains the code-behind for the PhotoDetailPage in the application.
//This page is designed to display detailed information about a selected photo, such as its title, author, and large image.

using FlickrSearchApp.Models;

namespace FlickrSearchApp

{
    public partial class PhotoDetailPage : ContentPage
    {
        public Command BackCommand { get; set; }
        public PhotoDetailPage()
        {
            InitializeComponent();
            BackCommand = new Command(async () => await GoBackAsync());
            // Set the BindingContext to the MainViewModel
            BindingContext = PhotoData.CurrentSelectedPhoto;
        }

        public async Task GoBackAsync()
        {
            Console.WriteLine("Back button clicked!");
            await DisplayAlert("Test", "Back button was clicked!", "OK");
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}