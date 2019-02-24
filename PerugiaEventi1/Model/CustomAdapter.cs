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
    public class ViewHolder : Java.Lang.Object {

        public Button Evento { get; set; }

    }

    public class CustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Evento> eventi;
        public CustomAdapter(Activity activity, List<Evento> eventi)
        {
            this.activity = activity;
            this.eventi = eventi;
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

        public override int GetItemViewType(int position)
        {
            return base.GetItemViewType(position);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }
    }
}