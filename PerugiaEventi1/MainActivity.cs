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
using Android.Views;
using System.Net;
using Android.Content;

namespace PerugiaEventi1
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity
    {
        Button bottoneCaricaEventi;
        ListView listaEventi;
        RootObject root;
        List<Contenuto> listaContenuti;
        string totaleContenuti;
        List<Evento> eventi;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            eventi = new List<Evento>();

            bottoneCaricaEventi = FindViewById<Button>(Resource.Id.bottoneCaricaEventi);
            bottoneCaricaEventi.Visibility = ViewStates.Gone;
            listaEventi = FindViewById<ListView>(Resource.Id.listaEventi);

            DateTime thisDay = DateTime.Now.Date;
            WebClient httpClient = new WebClient();
            var jsonData = httpClient.DownloadString("http://dati.umbria.it/dataset/410faa97-546b-4362-a6d7-f8794d18ed19/resource/8afe729a-0f59-4647-95ee-481577e83bea/download/eventijsonitit.zipeventiitit.json");


            root = JsonConvert.DeserializeObject<RootObject>(jsonData);
            totaleContenuti = root.Total;
            listaContenuti = root.Contenuto;
            //TODO aggiungi il controllo per non aggiornamento se
            //l'ultimo evento caricato non è cambiato 

            int x = int.Parse(totaleContenuti);

            for (int i = 0; i < x - 1; i++)
            {
                //vediamo di non caricare gli eventi che sono terminati 
                //- a quanto pare sono ordinati cronologicamente questi eventi
                if (DateTime.ParseExact(listaContenuti[i].Data_fine, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= thisDay)
                {
                    if (listaContenuti[i].Comune == "Perugia")
                    {
                        //TODO crea oggetto eventi
                        Evento nuovoEvento = new Evento(listaContenuti[i].Titolo,
                            listaContenuti[i].Id_contenuto, listaContenuti[i].Url_risorsa,
                            listaContenuti[i].Data_inizio, listaContenuti[i].Data_fine,
                            listaContenuti[i].Descrizione);
                        eventi.Add(nuovoEvento);
                    }
                }
            }


            listaEventi = FindViewById<ListView>(Resource.Id.listaEventi);
            var adapter = new CustomAdapter(this, eventi);
            listaEventi.Adapter = adapter;

            /*bottoneCaricaEventi.Click += delegate {
                //bottoneCaricaEventi.Text = "Sto caricando";

                WebClient httpClient = new WebClient();
                var jsonData = httpClient.DownloadString("http://dati.umbria.it/dataset/410faa97-546b-4362-a6d7-f8794d18ed19/resource/8afe729a-0f59-4647-95ee-481577e83bea/download/eventijsonitit.zipeventiitit.json");


                root = JsonConvert.DeserializeObject<RootObject>(jsonData);
                totaleContenuti = root.Total;
                listaContenuti = root.Contenuto;
                //TODO aggiungi il controllo per non aggiornamento se
                //l'ultimo evento caricato non è cambiato 

                int x = int.Parse(totaleContenuti);

                for (int i = 0; i < x-1; i++) {
                    //vediamo di non caricare gli eventi che sono terminati 
                    //- a quanto pare sono ordinati cronologicamente questi eventi
                    if(DateTime.ParseExact(listaContenuti[i].Data_fine, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= thisDay)
                    {
                        if (listaContenuti[i].Comune == "Perugia")
                        {
                            //TODO crea oggetto eventi
                            Evento nuovoEvento = new Evento(listaContenuti[i].Titolo, 
                                listaContenuti[i].Id_contenuto, listaContenuti[i].Url_risorsa,
                                listaContenuti[i].Data_inizio, listaContenuti[i].Data_fine,
                                listaContenuti[i].Descrizione);
                            eventi.Add(nuovoEvento);
                        }
                    }
                }

                
                listaEventi = FindViewById<ListView>(Resource.Id.listaEventi);
                var adapter = new CustomAdapter(this, eventi);
                listaEventi.Adapter = adapter;

                //gestiamo i click all'interno della listview
                //listaEventi.ItemClick += listaEventi_ItemClick;

                bottoneCaricaEventi.Visibility = ViewStates.Gone;
            };*/
        }

        /*private void listaEventi_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var titolo = eventi[e.Position].Titolo;
            //bisogna, immagino fare l'update della view con notify - prima creando un observer.
            Intent dettagli = new Intent(this, typeof(EventoInDettaglio));
            dettagli.PutExtra("titolo", titolo);
        }*/
    }
}