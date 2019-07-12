using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace PerugiaEventi1
{
    [Activity(Label = "EventoInDettaglio", NoHistory = false, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
   
    public class EventoInDettaglio : Activity
    {
        public TextView titoloEvento, urlView, inizioView, fineView, descrizioneView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.dettagliEvento);
            titoloEvento = FindViewById<TextView>(Resource.Id.dettagliTitolo);
            urlView = FindViewById<TextView>(Resource.Id.dettagliUrl);
            inizioView = FindViewById<TextView>(Resource.Id.dettagliInizio);
            fineView = FindViewById<TextView>(Resource.Id.dettagliFine);
            descrizioneView = FindViewById<TextView>(Resource.Id.dettagliDescrizione);
            titoloEvento.Text = Intent.GetStringExtra("titolo");
            urlView.Text = Intent.GetStringExtra("url");
            inizioView.Text = Intent.GetStringExtra("inizia");
            fineView.Text = Intent.GetStringExtra("finisce");
            descrizioneView.Text = Intent.GetStringExtra("descrizione");
            /*titoloEvento.Click += delegate
            {
                var uri = Android.Net.Uri.Parse(titoloEvento.Text);
                var intent = new Intent(Intent.ActionView, uri);
                ApplicationContext.StartActivity(intent);
            };*/
        }

        public override void OnBackPressed()
        {
            Intent tornaIndietro = new Intent(Application.Context, typeof(MainActivity));
            Application.Context.StartActivity(tornaIndietro);
            base.OnBackPressed();
        }
    }
}