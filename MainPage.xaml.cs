//The MainPage.xaml.cs file defines the code-behind for the main page of the application, MainPage.
//This page is responsible for the UI and binds data to a MainViewModel for MVVM (Model-View-ViewModel) architecture.

namespace FlickrSearchApp;

using Microsoft.Maui.Controls;
using FlickrSearchApp.Models;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}