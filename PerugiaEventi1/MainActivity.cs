﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Net;
using System.IO;
using PerugiaEventi1.Model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using Android.Views;
using System.Net;
using Android.Content;
using System.Threading;

namespace PerugiaEventi1
{
    // AlwaysRetainTaskState = true
    [Activity(Label = "PerugiaEventi", Theme = "@style/Theme.Design", MainLauncher = true, NoHistory =true,ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity
    {
        //Button bottoneCaricaEventi;
        static ListView listaEventi;
        public ScrollView scrollView2;
        static RootObject root;
        static List<Contenuto> listaContenuti;
        static string totaleContenuti;
        static List<Evento> eventi;
        static List<Evento> eventi_originali= new List<Evento>();
        public static string jsonData;
        static DateTime thisDay;
        static string previousSearch;
        static DateTime lastDownload; 
        static CustomAdapter adapter;
        private static ProgressBar circularbar;
        private static int progressStatus1 = 100;
        //static Button bottone;
        static Android.Support.V7.Widget.Toolbar toolbar;
        private static WebClient httpClient;
        static List<Evento> eventiDaRimuovere;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (savedInstanceState != null) {
                if (savedInstanceState.GetString("lastSearch")!=null) {
                    previousSearch = savedInstanceState.GetString("lastSearch");
                }
            }
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar1);
            toolbar.Visibility = ViewStates.Invisible;
           
            
                

            eventi = new List<Evento>();

            //bottoneCaricaEventi = FindViewById<Button>(Resource.Id.bottoneCaricaEventi);
            //bottoneCaricaEventi.Visibility = ViewStates.Gone;
            circularbar = FindViewById<ProgressBar>(Resource.Id.circularProgressbar);
            circularbar.Max = 100;
            circularbar.Progress = 100;
            circularbar.SecondaryProgress = 100;





           
            thisDay = DateTime.Now.Date;

            //bottone = FindViewById<Button>(Resource.Id.button1);
            //bottone.Visibility = ViewStates.Invisible;
            
            httpClient = new WebClient();

            httpClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCallback2);
            //httpClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
            listaEventi = FindViewById<ListView>(Resource.Id.listaEventi);
            adapter = new CustomAdapter(this, eventi);
            if (jsonData == null || !thisDay.Equals(lastDownload))
            {
                httpClient.DownloadStringAsync(new System.Uri("http://dati.umbria.it/dataset/410faa97-546b-4362-a6d7-f8794d18ed19/resource/8afe729a-0f59-4647-95ee-481577e83bea/download/eventijsonitit.zipeventiitit.json"));
                new System.Threading.Thread(new ThreadStart(delegate
                {
                    while (httpClient.IsBusy)
                    {
                        if (progressStatus1 != 0)
                            progressStatus1 -= 1;
                        else
                        {
                            progressStatus1 += 100;
                        }
                        circularbar.Progress = progressStatus1;
                        System.Threading.Thread.Sleep(10);
                    }
                })).Start();
            }
            else {
                
                circularbar.Visibility = ViewStates.Gone;
                InizializzaVista(jsonData);

            }

            

            
        }

        private static void InizializzaVista(string jsonData)
        {
            
            root = JsonConvert.DeserializeObject<RootObject>(jsonData);
               totaleContenuti = root.Total;
               //Console.WriteLine("Il numero totale dei bottoni: "+ totaleContenuti);
               listaContenuti = root.Contenuto;
            //TODO aggiungi il controllo per non aggiornamento se
            //l'ultimo evento caricato non è cambiato 
               //int numeroEventiPerugia = 0;
               int x = int.Parse(totaleContenuti);

               for (int i = 0; i < x - 1; i++)
               {
                   //vediamo di non caricare gli eventi che sono terminati 
                   //- a quanto pare sono ordinati cronologicamente questi eventi
                   if (DateTime.ParseExact(listaContenuti[i].Data_fine, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= thisDay)
                   {
                       if (listaContenuti[i].Comune == "Perugia")
                       {
                           //numeroEventiPerugia += 1;
                           //TODO crea oggetto eventi
                           Evento nuovoEvento = new Evento(listaContenuti[i].Titolo,
                               listaContenuti[i].Id_contenuto, listaContenuti[i].Url_risorsa,
                               listaContenuti[i].Data_inizio, listaContenuti[i].Data_fine,
                               listaContenuti[i].Descrizione);
                           eventi.Add(nuovoEvento);
                           eventi_originali.Add(nuovoEvento);
                    }
                   }
               }

            
            listaEventi.Adapter = adapter;
            //bottone.Visibility = ViewStates.Visible;
            toolbar.Visibility = ViewStates.Visible;
            if (previousSearch != null) {
                CercaEventi(previousSearch);
            }
            //Toast.MakeText(Application.Context, "Il numero totale dei bottoni: " + numeroEventiPerugia, ToastLength.Long).Show();

        }

        private static void CercaEventi(string previousSearch)
        {
           
            DateTime time = DateTime.ParseExact(previousSearch, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (var i = 0; i < eventi.Count; i++)
            {
                if (!(DateTime.ParseExact(eventi[i].Inizio, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.ParseExact(time.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) &&
                DateTime.ParseExact(eventi[i].Fine, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= DateTime.ParseExact(time.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                {
                    eventiDaRimuovere.Add(eventi[i]);
                }
            }
            for (var i = 0; i < eventiDaRimuovere.Count; i++)
            {
                eventi.Remove(eventiDaRimuovere[i]);
                //adapter.NotifyDataSetChanged();
            }
            
            adapter.NotifyDataSetChanged();

            //adapter.NotifyDataSetChanged();
            //System.Threading.Thread.Sleep(1000);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.menu_main, menu);
            IMenuItem mSearch = menu.FindItem(Resource.Id.searchview);
            Android.Support.V7.Widget.SearchView mSearchView = (Android.Support.V7.Widget.SearchView)mSearch.ActionView;
            mSearchView.Visibility = ViewStates.Gone;
           
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (httpClient.IsBusy){
                return true;
            }
            if (id == Resource.Id.action_search)
            {
                
                Toast.MakeText(Application.Context, "Search clicked", ToastLength.Long).Show();
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    if (eventiDaRimuovere != null)
                    {
                        for (var i = 0; i < eventiDaRimuovere.Count; i++)
                        {
                            eventi.Add(eventiDaRimuovere[i]);
                            //adapter.NotifyDataSetChanged();
                        }
                    }
                    

                    eventiDaRimuovere = new List<Evento>();
                    previousSearch = time.ToShortDateString();
                    for (var i = 0; i < eventi.Count; i++) {
                        if (!(DateTime.ParseExact(eventi[i].Inizio, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.ParseExact(time.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) &&
                        DateTime.ParseExact(eventi[i].Fine, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= DateTime.ParseExact(time.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture))) {
                            eventiDaRimuovere.Add(eventi[i]);
                        }
                    }
                    
                    for (var i = 0; i < eventiDaRimuovere.Count; i++) {
                        eventi.Remove(eventiDaRimuovere[i]);
                        //adapter.NotifyDataSetChanged();
                        
                    }
                    
                    adapter.NotifyDataSetChanged();
                    Toast.MakeText(Application.Context, time.ToShortDateString(), ToastLength.Long).Show();
                    //adapter.NotifyDataSetChanged();


                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
                return true;
            }
            else if (id == Resource.Id.action_unsearch){
                previousSearch = null;
                if (eventiDaRimuovere != null)
                {   //eventi.Clear();
                    //adapter.NotifyDataSetChanged();
                    for (var i = 0; i < eventiDaRimuovere.Count; i++)
                    {   
                        eventi.Add(eventiDaRimuovere[i]);
                        
                    }
                
                }
                eventiDaRimuovere = null;

                adapter.NotifyDataSetChanged();
                
                Toast.MakeText(Application.Context, "Unsearch clicked", ToastLength.Long).Show();
                return true;
            }
            else {  }
            return base.OnOptionsItemSelected(item);
        }

       

        private static void DownloadStringCallback2(Object sender, DownloadStringCompletedEventArgs e)
{    // If the request was not canceled and did not throw
    // an exception, display the resource.
           if (!e.Cancelled && e.Error == null)
           {
               lastDownload = thisDay;
               jsonData = e.Result;
                //circularbar.Visibility = ViewStates.Invisible;
                InizializzaVista(jsonData);
                circularbar.Visibility = ViewStates.Gone;



            }
            else {
                Toast.MakeText(Application.Context, "Can't download events! Check your Internet connection", ToastLength.Long).Show();
            }
}
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (previousSearch != null)
            {
                outState.PutString("lastSearch", previousSearch);
            }
            base.OnSaveInstanceState(outState);
        }
        public override void OnBackPressed()
        {
            previousSearch = null;
            Activity activity = (Activity)this;
            activity.FinishAffinity();
            base.OnBackPressed();
        }
    }
}