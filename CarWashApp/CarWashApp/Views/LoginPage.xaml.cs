using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarWashApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnRegistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }

        //private void BtnIniciarSesion_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        btnIniciarSesion.IsEnabled = false;
        //        Device.StartTimer(TimeSpan.FromSeconds(3), () =>
        //        {
        //            progreso.IsRunning = true;
        //            btnIniciarSesion.IsEnabled = false;
        //            return false;
        //        });
        //        Device.StartTimer(TimeSpan.FromSeconds(10), () =>//Este se resta con 3 y queda un total de 7 segundos
        //        {
        //            progreso.IsRunning = false;
        //            btnIniciarSesion.IsEnabled = true;
        //            return false;
        //        });
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}