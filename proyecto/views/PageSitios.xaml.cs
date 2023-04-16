using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageSitios : ContentPage
	{
        
        // Variable global de la foto
        Plugin.Media.Abstractions.MediaFile filefoto = null;

        //Function que convierta imagen to base64
        private String ImagetoBase64()
        {
            if (filefoto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = filefoto.GetStream();
                    stream.CopyTo(memory);
                    byte[] bytefoto = memory.ToArray();
                    string fotoBase64 = Convert.ToBase64String(bytefoto);
                    return fotoBase64;
                }
            }
            return null;
        }
        public PageSitios ()
		{
			InitializeComponent ();
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var pagesiti = new views.PageReSitio();
            Navigation.PushAsync(pagesiti);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    txtlatitud.Text = Convert.ToString(location.Latitude);
                    txtlongitud.Text = Convert.ToString(location.Longitude);

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception ex)
            {
            }

        }
        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
 
            var sit = new Models.Sitios
            {
                nombre = txtnombre.Text,
                pais = cbpais.SelectedItem.ToString(),
                latitud = txtlatitud.Text,
                longitud = txtlongitud.Text,
                nota = txtnota.Text,
                foto = ImagetoBase64()

            };


            if (await App.DBsit.Savesitio(sit) > 0)
                await DisplayAlert("Aviso", "Lugar Registrado", "OK");
            else
                await DisplayAlert("Aviso", "Error", "OK");

            var page = new views.PageReSitio();
            page.BindingContext = sit;
            await Navigation.PushAsync(page);

            
            this.txtnombre.Text = "";
            this.cbpais.SelectedItem = "";
            this.txtlatitud.Text = "";
            this.txtlongitud.Text = "";
            this.txtnota.Text = "";
        }

        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
          filefoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "MiApp",
                Name = "foto.jpg"
            });

            if (filefoto != null)
            {
                foto.Source = ImageSource.FromStream(() => { return filefoto.GetStream(); });
            }
        }
    }
}