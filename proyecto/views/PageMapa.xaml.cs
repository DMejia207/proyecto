using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace proyecto.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMapa : ContentPage
    {
        public PageMapa()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    var posicion = new Position(location.Latitude, location.Longitude);
                    mapa.MoveToRegion(MapSpan.FromCenterAndRadius(posicion, Distance.FromKilometers(1)));
                }
            }
            catch (Exception ex)
            {
            }

        }
    }
}