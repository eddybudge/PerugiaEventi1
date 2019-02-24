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

namespace PerugiaEventi1.Model
{
    public class AltriIndirizzi
    {
        public string Comune { get; set; }
        public Coordinate2 Coordinate { get; set; }
    }
}