using CarListApp.Helpers;
using CarListApp.Models;
using CarListApp.Services;
using CarListApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;
        private CarAPIService carAPIService;



        public LoginViewModel(CarAPIService carAPIService)
        {
            this.carAPIService = carAPIService;
        }

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayLoginMessage("Invalid Login Attempt");
            }
            else
            {
                // Call API to attempt a login
                var loginModel = new LoginModel(username, password);

                var response = await carAPIService.Login(loginModel);

                // display message
                await DisplayLoginMessage(carAPIService.StatusMessage);

                if (!string.IsNullOrEmpty(response.AccessToken))
                {
                    // Store token in secure storage 
                    await SecureStorage.SetAsync("Token", response.AccessToken);

                    // build a menu on the fly...based on the user role
                    var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.AccessToken) as JwtSecurityToken;

                    var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;

                    App.UserInfo = new UserInfo()
                    {
                        Username = Username,
                        Role = role
                    };


                    // navigate to app's main page
                    MenuBuilder.BuildMenu();
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");

                }
                else
                {
                    await DisplayLoginMessage("Invalid Login Attempt");
                }
            }
        }

        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Login Attempt Result", message, "OK");
            Password = string.Empty;
        }
    }
}
