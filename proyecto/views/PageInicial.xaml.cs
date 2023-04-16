using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInicial : ContentPage
    {

        public PageInicial()
        {
            InitializeComponent();
        }
        private void btnagenda_Clicked(object sender, EventArgs e)
        {
  
          var pagecontac = new views.PageContactos();
            Navigation.PushAsync(pagecontac);
        }
        private void btnsitios_Clicked(object sender, EventArgs e)
        {

            var pagesitio = new views.PageSitios();
            Navigation.PushAsync(pagesitio);
        }

         
    }
}