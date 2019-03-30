using CarWashApp.Helper;
using CarWashApp.Models;
using CarWashApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarWashApp.ViewsModels
{
    public class RegisterViewModel:APIServices
    {
        Clientes clientes = new Clientes();
        APIServices aPIServices = new APIServices();
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
                        var isSuccess = await aPIServices.RegisterAsync(
                        Email, Password, ConfirmPassword);
                        Settings.Username = Email;
                        Settings.Password = Password;
                    if (isSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("OK", "Usuario Creado con exito", "OK");
                            IsRunning = false;
                            IsState = true;
                        //regisetrView.Clean();
                    } else if (Email == null || Password == null || ConfirmPassword == null||clientes.Nombre==null
                        || clientes.Apellido==null||clientes.Telefono==null)
                        {
                            await App.Current.MainPage.DisplayAlert("Informacion", "Todos los campos son obligatorios", "OK");
                            IsRunning = false;
                            IsState = true;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("OK", "Error al registrarse", "OK");
                            IsRunning = false;
                            IsState = true;
                            //regisetrView.Clean();
                            //Message ="Error al registrarse";
                        }
                    }
                    catch (Exception)
                    {

                        await App.Current.MainPage.DisplayAlert("OK", "El usuario ya existe", "OK");
                        IsRunning = false;
                        IsState = true;
                        //regisetrView.Clean();
                    }
                });

            }

        }

    }
}
