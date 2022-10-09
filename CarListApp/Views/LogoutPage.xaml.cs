using CarListApp.ViewModels;

namespace CarListApp.Views;

public partial class LogoutPage : ContentPage
{
	public LogoutPage(LogoutViewModel logoutViewModel)
	{
		InitializeComponent();

        Content = new VerticalStackLayout
        {
            Children = {
                new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Logging Out"
                }
            }
        };

        BindingContext = logoutViewModel;
    }
}