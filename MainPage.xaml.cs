namespace FlickrSearchApp;
using Microsoft.Maui.Controls;
 using FlickrSearchApp.Models;
 using FlickrSearchApp.Services;

public partial class MainPage : ContentPage
{
    private readonly FlickrService _flickrService;

    // Use constructor injection for FlickrService
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

