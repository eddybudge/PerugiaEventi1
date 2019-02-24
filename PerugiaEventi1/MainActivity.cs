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
using System;

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

            DateTime thisDay = DateTime.Today;

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

                int x = int.Parse(totaleContenuti);

                for (int i = 1; i < x; i++) {
                    if (listaContenuti[i].Comune == "Perugia") {
                        //vediamo di non caricare gli eventi che sono terminati
                        if (Convert.ToDateTime(listaContenuti[i].Data_fine) >= thisDay) {
                            //TODO crea oggetto eventi
                        }
                    }
                }
            };
        }

    }
}