using CarWashApp.Helper;
using CarWashApp.Models;
using CarWashApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarWashApp.ViewsModels
{
    public class ClientesViewModel : INotifyPropertyChanged
    {
        APIServices aPIServices = new APIServices();
        private List<Clientes> clientes;

        //public string AccesToken { get; set; }
        public List<Clientes> Clientes { get { return clientes; } set { clientes = value; OnPrpertyChanged(); } }

        public ICommand GetClientes
        {
            get
            {
                return new Command(async () =>
                {
                    var AccessToken = Settings.AccessToken;
                    Clientes = await aPIServices.GetClientesAsysnc(AccessToken);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPrpertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                (propertyName));
        }

    }
}
