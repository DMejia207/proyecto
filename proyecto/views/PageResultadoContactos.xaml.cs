using proyecto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;
using proyecto.Controllers;

namespace proyecto.views
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageResultadoContactos : ContentPage
    {
        public PageResultadoContactos()
        {
            InitializeComponent();
        }

        private async void Btneliminar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "¿Desea eliminar este Contacto?", "Si", "No"))
            {
                var contacto = (Contacto)(sender as MenuItem).CommandParameter;
                var result = await App.DBCon.DeleteCon(contacto);

                if (result == 1)
                {
                    listaa.ItemsSource = await App.DBCon.GetListcon();
                }
            }
        }

        private async void Btnllamar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "¿Desea realizar esta Llamada?", "Si", "No"))
            {
                var contacto = (Contacto)(sender as MenuItem).CommandParameter;
                var phonecall = CrossMessaging.Current.PhoneDialer;
                if (phonecall.CanMakePhoneCall)
                {
                    int num = Convert.ToInt32(contacto.telefono);
                    PhoneDialer.Open(contacto.telefono);
                }
                else
                {
                    await DisplayAlert("Aviso", "No se pudo realizar la llamada", "Ok");
                }
            }
        }

        private async void Btnactualizar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "¿Desea actualizar este Contacto?", "Si", "No"))
            {
                var contacto = (Contacto)(sender as MenuItem).CommandParameter;

                await Navigation.PushAsync(new PageActualizarContacto());
            }
        }

        private async void Btncompartir_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Confirmacion", "¿Desea compartir este Contacto?", "Si", "No");
            
                var contacto = (Contacto)(sender as MenuItem).CommandParameter;
                try
                {                      
                            await Share.RequestAsync(new ShareTextRequest()
                            {
                                Title = "Compartir Contacto",
                                Subject = "Contacto Compartido con Exito",
                                Text = contacto.nombre + "\n" + contacto.telefono.ToString()
                            });                                        
                }
                catch (Exception ex)
                {

                }           
        }

 

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listaa.ItemsSource = await App.DBCon.GetListcon();
        }
    }
}