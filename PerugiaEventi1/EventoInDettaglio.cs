using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PerugiaEventi1
{
    [Activity(Label = "EventoInDettaglio")]
    public class EventoInDettaglio : Activity
    {
        public TextView titoloEvento;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.dettagliEvento);
            titoloEvento = FindViewById<TextView>(Resource.Id.dettagliTitolo);
            titoloEvento.Text = Intent.GetStringExtra("url");
            titoloEvento.Click += delegate
            {
                var uri = Android.Net.Uri.Parse(titoloEvento.Text);
                var intent = new Intent(Intent.ActionView, uri);
                ApplicationContext.StartActivity(intent);
            };
        }
    }
}