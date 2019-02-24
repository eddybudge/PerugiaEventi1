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
    
    public class RootObject
    {
        public string Tipo { get; set; }
        public string Total { get; set; }
        public string Lingua { get; set; }
        public List<Contenuto> Contenuto { get; set; }
    }
}