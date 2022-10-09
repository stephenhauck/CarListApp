using CarListApp.Services;
using CarListApp.ViewModels;
using CarListApp.Views;

namespace CarListApp;

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

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CarDatabaseService>(s, dbPath));

        builder.Services.AddSingleton<CarListViewModel>();
        builder.Services.AddTransient<CarDetailsViewModel>();
        builder.Services.AddSingleton<LoadingViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LogoutViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<CarDetailsPage>();
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LogoutPage>();
        builder.Services.AddTransient<CarAPIService>();

        return builder.Build();
	}
}
