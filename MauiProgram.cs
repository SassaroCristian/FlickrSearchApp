using Microsoft.Extensions.Logging;
using FlickrSearchApp.Models;
using FlickrSearchApp.Services;
namespace FlickrSearchApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
			builder.Services.AddSingleton<SettingsService>();
			builder.Services.AddSingleton<FlickrService>();
			builder.Services.AddSingleton<MainViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
