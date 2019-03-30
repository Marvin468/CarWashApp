using CarWashApp.Views;
using CarWashApp.ViewsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Diagnostics;
using CarWashApp.Helper;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CarWashApp.Services
{
    public class LoginViewModel:APIServices
    {
        
        public string Username { get; set; } = null;
        public string Password { get; set; } = null;

        
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRunning = true;
                    IsState = false;
                    Username = Username;
                    Password = Password;
                    try
                    {
                        var KeyValues = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("username",Username),
                            new KeyValuePair<string, string>("password",Password),
                            new KeyValuePair<string, string>("grant_type","password")
                        };
                        var request = new HttpRequestMessage(
                        HttpMethod.Post, "http://gnstecnology-001-site3.etempurl.com/Token");
                        request.Content = new FormUrlEncodedContent(KeyValues);
                        var clientes = new HttpClient();
                        var response = await clientes.SendAsync(request);
                        var jwt = await response.Content.ReadAsStringAsync();
                        JObject jwtDinamic = JsonConvert.DeserializeObject<dynamic>(jwt);
                        var accessToken = jwtDinamic.Value<string>("access_token");
                        Settings.AccessToken = accessToken;
                        Debug.Write(jwt);
                        
                        if (response.IsSuccessStatusCode)
                        {
                            LoginPage loginPage = new LoginPage();
                            App.Current.Quit();
                            await App.Current.MainPage.Navigation.PushAsync(new PrincipalPage());
                            IsRunning = false;
                            IsState = true;
                        }else if (Username == null||Password==null)
                        {
                            await App.Current.MainPage.DisplayAlert("Informacion", "Digite su correo y contraseña", "OK");
                            IsRunning = false;
                            IsState = true;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Informacion", "El usuario o la contraseña son incorrectos", "OK");
                            IsRunning = false;
                            IsState = true;
                        }
                    }
                    catch (Exception)
                    {
                        await App.Current.MainPage.DisplayAlert("Informacion", "Revise su conexion a Internet", "OK");
                        IsRunning = false;
                        IsState = true;
                    }
                });
            }
        }

        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;
        }
    }
}
