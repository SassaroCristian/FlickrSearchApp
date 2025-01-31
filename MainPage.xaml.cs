namespace FlickrSearchApp;
using Microsoft.Maui.Controls;
 using FlickrSearchApp.Models;
 using FlickrSearchApp.Services;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        var flickrService = DependencyService.Get<FlickrService>();
        var viewModel = new MainViewModel(flickrService); 
        this.BindingContext = viewModel;
    }
}

