﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace PerugiaEventi1.Model
{
    public class ViewHolder : Java.Lang.Object
    {

        public Button bottoneEvento { get; set; }

    }

    public class CustomAdapter : BaseAdapter<Evento>
    {
        private Activity activity;
        private List<Evento> eventi;
        
        public CustomAdapter(Activity activity, List<Evento> eventi)
        {
            this.activity = activity;
            this.eventi = eventi;
        }

        public override Evento this[int position]
        {
            get { return eventi[position]; }
        }
        public override int Count => eventi.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return long.Parse(eventi[position].Id);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_item, parent, false);
            var bottoneEvento = view.FindViewById<Button>(Resource.Id.bottoneEventAdapter);
            
            bottoneEvento.Text = eventi[position].Titolo;
            //bottoneEvento.Tag = position;  --forse non serve
            if (!bottoneEvento.HasOnClickListeners)
            {
                bottoneEvento.Click += (sender, args) =>
                {
                    System.Diagnostics.Debug.WriteLine("Position: " + position + " Number of elements inside the list: " + eventi.Count());
                    //bisogna, immagino fare l'update della view con notify - prima creando un observer.
                    Intent dettagli = new Intent(Application.Context, typeof(EventoInDettaglio));
                    dettagli.PutExtra("titolo", bottoneEvento.Text);
                    dettagli.PutExtra("url", eventi[position].Url);
                    dettagli.PutExtra("inizia", eventi[position].Inizio);
                    dettagli.PutExtra("finisce", eventi[position].Fine);
                    dettagli.PutExtra("descrizione", eventi[position].Descrizione);
                    Application.Context.StartActivity(dettagli);
                };
            }

            return view;
        }

    }
}