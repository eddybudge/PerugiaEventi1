using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Net;
using System.IO;
using PerugiaEventi1.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PerugiaEventi1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button bottoneCaricaEventi;
        ListView listaEventi;
        string response;
        RootObject root;
        List<Contenuto> listaContenuti;
        Contenuto primoContenuto;
        string totaleContenuti;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            bottoneCaricaEventi = FindViewById<Button>(Resource.Id.bottoneCaricaEventi);
            listaEventi = FindViewById<ListView>(Resource.Id.listaEventi);
            bottoneCaricaEventi.Click += delegate {
                //bottoneCaricaEventi.Text = "Sto caricando";
               
                //carica json locale e deserializzalo

                StreamReader strm = new StreamReader(Assets.
                    Open("eventijsonitit.zipeventiitit.json"));
                response = strm.ReadToEnd();

                root = JsonConvert.DeserializeObject<RootObject>(response);
                totaleContenuti = root.Total;
                listaContenuti = root.Contenuto;
                primoContenuto = listaContenuti[0];
                bottoneCaricaEventi.Text = primoContenuto.Titolo;
            };
        }

    }
}