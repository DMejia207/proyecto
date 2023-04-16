using proyecto.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageReSitio : ContentPage
    {
        public PageReSitio()
        {
            InitializeComponent();
        }


        private async void Btncompartir_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Confirmacion", "¿Desea compartir la Ubicacion?", "Si", "No");

            var sitios = (Sitios)(sender as MenuItem).CommandParameter;
            try
            {
                await Share.RequestAsync(new ShareTextRequest()
                {
                    Title = "Compartir Contacto",
                    Subject = "Contacto Compartido con Exito",
                    Text = sitios.nombre + "\n" + sitios.pais + "\n" + sitios.latitud + "\n" + sitios.longitud.ToString()
                });
            }
            catch (Exception ex)
            {

            }
        }

        private async void Btneliminar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "¿Desea eliminar el sitio?", "Si", "No"))
            {
                var sitios = (Sitios)(sender as MenuItem).CommandParameter;
                var result = await App.DBsit.Deletesitio(sitios);
                
                if (result==1)
                {
                    lista.ItemsSource = await App.DBsit.GetListsitio();
                }
            }
        }

        private async void Btnactualizar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "¿Desea actualizar el Sitio?", "Si", "No"))
            {
                var sitios = (Sitios)(sender as MenuItem).CommandParameter;

                await Navigation.PushAsync(new PageActualizarSitio());
            }
        }

        private async void Btnubicacion_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "¿Desea abrir el Mapa?", "Si", "No"))
            {
                var sitios = (Sitios)(sender as MenuItem).CommandParameter;

                await Navigation.PushAsync(new PageMapa());
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            lista.ItemsSource = await App.DBsit.GetListsitio();
        }
    }
}