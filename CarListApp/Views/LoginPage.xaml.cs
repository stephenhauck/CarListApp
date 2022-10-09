using CarListApp.ViewModels;

namespace CarListApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginPageViewModel)
	{
		InitializeComponent();
        this.BindingContext = loginPageViewModel;
    }
}