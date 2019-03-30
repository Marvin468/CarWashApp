using CarWashApp.Models;
using CarWashApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarWashApp.Services
{
    public class APIServices:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(
         [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(
                    propertyName));
        }
        public bool IsRunning
        {
            get { return IsBusy_BF; }
            set { IsBusy_BF = value; OnPropertyChanged(); }
        }
        public bool IsBusy_BF { get; set; }
        public bool IsState
        {
            get { return IsState_; }
            set { IsState_ = value; OnPropertyChanged(); }
        }
        public bool IsState_ { get; set; } = true;
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            var clients = new HttpClient();
            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await clients.PostAsync("http://gnstecnology-001-site3.etempurl.com/api/Account/Register", content);
            return response.IsSuccessStatusCode;
        }
        //public async Task<bool> RegisterAsyncPersonal(string Nombre, string Apellido, string Telefono,string Direccion)
        //{
        //    var clients = new HttpClient();
        //    var model = new Clientes
        //    {
        //        Nombre=Nombre,
        //        Apellido=Apellido,
        //        Telefono=Telefono,
        //        Direccion=Direccion
        //    };

        //    var json = JsonConvert.SerializeObject(model);
        //    HttpContent content = new StringContent(json);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    //var response = await clients.PostAsync("http://gnstecnology-001-site3.etempurl.com/api/, content);
        //    //return response.IsSuccessStatusCode;
        //}
        //public async Task<bool> LoginAsync(string userName, string password)
        //{
        //    try
        //    {
        //        var KeyValues = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("username",userName),
        //        new KeyValuePair<string, string>("password",password),
        //        new KeyValuePair<string, string>("grant_type","password")
        //    };
        //        var request = new HttpRequestMessage(
        //        HttpMethod.Post, "http://gnstecnology-001-site3.etempurl.com/Token");
        //        request.Content = new FormUrlEncodedContent(KeyValues);
        //        var clientes = new HttpClient();
        //        var response = await clientes.SendAsync(request);
        //        var content = await response.Content.ReadAsStringAsync();
        //        return response.IsSuccessStatusCode;
        //    }
        //    catch (Exception)
        //    {
        //       // await App.Current.MainPage.DisplayAlert("Informacion", "Error de conexion a internet", "OK");
        //    }
        //    return false;
        //}
        public async Task<List<Clientes>> GetClientesAsysnc(string accesstoken)
        {
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accesstoken);
            var json = await cliente.GetStringAsync("http://gnstecnology-001-site3.etempurl.com/api/IdentityModelsClientes");
            var clientes = JsonConvert.DeserializeObject<List<Clientes>>(json);
            return clientes;
        }

    }

}

