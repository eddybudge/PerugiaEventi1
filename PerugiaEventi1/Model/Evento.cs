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
    public class Evento
    {
        public Evento(string titolo, string id)
        {
            Titolo = titolo;
            Id = id;
        }
      
        
        public string Id { get; set; }
        public string Titolo { get; set; }
        //public string Data_fine { get; set; }
    }
}