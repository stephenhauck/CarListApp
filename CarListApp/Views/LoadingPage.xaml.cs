using CarListApp.ViewModels;

namespace CarListApp.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingViewModel loadingPageViewModel)
	{
		InitializeComponent();
        this.BindingContext = loadingPageViewModel;
    }
}