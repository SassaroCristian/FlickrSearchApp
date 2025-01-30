namespace FlickrSearchApp;
using Microsoft.Maui.Controls;
 using FlickrSearchApp.Models;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}
}

