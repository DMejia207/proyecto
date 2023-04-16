using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageActualizarContacto : ContentPage
    {

        // Variable global de la foto
        Plugin.Media.Abstractions.MediaFile filefotoo = null;

        //Function que convierta imagen to base64
        private String ImagetoBase64()
        {
            if (filefotoo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = filefotoo.GetStream();
                    stream.CopyTo(memory);
                    byte[] bytefoto = memory.ToArray();
                    string fotoBase64 = Convert.ToBase64String(bytefoto);
                    return fotoBase64;
                }
            }
            return null;
        }
        public PageActualizarContacto()
        {
            InitializeComponent();
        }

        protected async void btncontactos_Clicked(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtnombre.Text))
            {
                await DisplayAlert("Alerta", "Debe escribir un nombre", "OK");
                txtnombre.Focus();
            }

            else
            if (String.IsNullOrEmpty(txttelefono.Text))

            {
                await DisplayAlert("Alerta", "Debe escribir un telefono", "OK");
                txttelefono.Focus();

            }
            else
             if (String.IsNullOrEmpty(txtnota.Text))
            {
                await DisplayAlert("Alerta", "Debe escribir una nota", "OK");
                txttelefono.Focus();
            }
            else

            {
                var con = new Models.Contacto
                {
                    id = Convert.ToInt32(Id.Text),
                    nombre = txtnombre.Text,
                    telefono = txttelefono.Text,
                    edad = int.Parse(edad.Text),
                    pais = cbpais.SelectedItem.ToString(),
                    nota = txtnota.Text,
                    fotoo = ImagetoBase64()

                };


                if (await App.DBCon.SaveCon(con) > 0)
                    await DisplayAlert("Aviso", "Contacto Actualizado", "OK");
                else
                    await DisplayAlert("Aviso", "Error", "OK");

                var page = new views.PageResultadoContactos();
                page.BindingContext = con;
                await Navigation.PushAsync(page);
            }
        }

        private async void btnfotoo_Clicked(object sender, EventArgs e)
        {
            filefotoo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "MiAppp",
                Name = "fotoo.jpg"
            });

            if (filefotoo != null)
            {
                fotoo.Source = ImageSource.FromStream(() => { return filefotoo.GetStream(); });
            }
        }
    }
}
    
