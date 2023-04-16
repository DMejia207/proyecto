using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace proyecto
{
    public partial class App : Application
    {
        static Controllers.ContactosController dbcon;

        public static Controllers.ContactosController DBCon
        {
            get
            {
                if (dbcon == null)
                {
                    var dbpathh = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    var dbnamee = "con.db3";
                    dbcon = new Controllers.ContactosController(Path.Combine(dbpathh, dbnamee));
                }

                return dbcon;
            }
        }

        static Controllers.SitiosController dbsit;
         
        public static Controllers.SitiosController DBsit
        {
            get
            {
                if (dbsit == null)
                {
                    var dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    var dbname = "sit.db3";
                    dbsit = new Controllers.SitiosController(Path.Combine(dbpath, dbname));
                }

                return dbsit;
            }
        }
        public App()
        {

            InitializeComponent();

            MainPage = new NavigationPage(new views.PageInicial());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
