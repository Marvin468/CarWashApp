using CarWashApp.Helper;
using CarWashApp.Models;
using CarWashApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarWashApp.ViewsModels
{
    public class RegisterViewModel:APIServices
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }
        public ICommand RegisterComand
        {

            get
            {
                return new Command(async () =>
                {
                    IsRunning = true;
                    IsState = false;
                    try
                    {
                        var clients = new HttpClient();
                        var model = new RegisterBindingModel
                        {
                            Email = Email,
                            Password = Password,
                            ConfirmPassword = ConfirmPassword
                        };

                        var json = JsonConvert.SerializeObject(model);
                        HttpContent content = new StringContent(json);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var response = await clients.PostAsync("http://gnstecnology-001-site3.etempurl.com/api/Account/Register", content);
                        Settings.Username = Email;
                        Settings.Password = Password;
                        if (response.IsSuccessStatusCode)
                        {
                            await App.Current.MainPage.DisplayAlert("OK", "Usuario Creado con exito", "OK");
                            IsRunning = false;
                            IsState = true;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("OK", "El usuario ya existe, inicia sesion", "OK");
                            IsRunning = false;
                            IsState = true;
                        }
                    }
                    catch (Exception)
                    {
                        await App.Current.MainPage.DisplayAlert("Informacion", "Verifique su conexion a internet", "OK");
                        IsRunning = false;
                        IsState = true;
                    }
                    
                });

            }

        }

    }
}
