using CarListApp.Helpers;
using CarListApp.Models;
using CarListApp.Views;
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
    public class LoadingViewModel : BaseViewModel
    {
        public LoadingViewModel()
        {
            CheckUserLoginDetails();
            
        }

        private async void CheckUserLoginDetails()
        {
            //retrieve token from internal storage
            var token = await SecureStorage.GetAsync("AccessToken");
            if(string.IsNullOrEmpty(token))
            {
                await GoToLoginPage();
            }
            else
            {
                var jsonToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

                if (jsonToken.ValidTo < DateTime.UtcNow)
                {
                    SecureStorage.Remove("Token");
                    await GoToLoginPage();
                }
                else
                {
                    var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;

                    App.UserInfo = new UserInfo()
                    {
                        Username = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Email))?.Value,
                        Role = role
                    };
                    MenuBuilder.BuildMenu();
                    await GoToMainPage();
                }
            }


        }

        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        private async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
    }
}
