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
using System.Globalization;

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
        List<Evento> eventi;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            eventi = new List<Evento>();

            bottoneCaricaEventi = FindViewById<Button>(Resource.Id.bottoneCaricaEventi);
            listaEventi = FindViewById<ListView>(Resource.Id.listaEventi);

            DateTime thisDay = DateTime.Now.Date;

            bottoneCaricaEventi.Click += delegate {
                //bottoneCaricaEventi.Text = "Sto caricando";
               
                //carica json locale e deserializzalo

                StreamReader strm = new StreamReader(Assets.
                    Open("eventijsonitit.zipeventiitit.json"));
                response = strm.ReadToEnd();

                root = JsonConvert.DeserializeObject<RootObject>(response);
                totaleContenuti = root.Total;
                listaContenuti = root.Contenuto;
                primoContenuto = listaContenuti[9];
                //TODO aggiungi il controllo per non aggiornamento se
                //l'ultimo evento caricato non è cambiato 
                bottoneCaricaEventi.Text = primoContenuto.Data_inizio;

                int x = int.Parse(totaleContenuti);
                /*for (int i = 0; i < 7; i++) {
                    Evento nuovoEvento = new Evento("Evento#"+i,
                                ""+i);
                    eventi.Add(nuovoEvento);
                }*/

                for (int i = 0; i < x-1; i++) {
                    //vediamo di non caricare gli eventi che sono terminati 
                    //- a quanto pare sono ordinati cronologicamente questi eventi
                    if(DateTime.ParseExact(listaContenuti[i].Data_fine, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= thisDay)
                    {
                        if (listaContenuti[i].Comune == "Perugia")
                        {
                            //TODO crea oggetto eventi
                            Evento nuovoEvento = new Evento(listaContenuti[i].Titolo, 
                                listaContenuti[i].Id_contenuto);
                            eventi.Add(nuovoEvento);
                        }
                    }
                }

                
                listaEventi = FindViewById<ListView>(Resource.Id.listaEventi);
                var adapter = new CustomAdapter(this, eventi);
                listaEventi.Adapter = adapter;
            };
        }

    }
}