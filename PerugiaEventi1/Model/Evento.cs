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
        public Evento(string titolo, string id, string url, string inizio, string fine, string descrizione)
        {
            Titolo = titolo;
            Id = id;
            Url = url;
            Inizio = inizio;
            Fine = fine;
            Descrizione = descrizione;
        }
       
        public string Url { get; set; }
        public string Id { get; set; }
        public string Titolo { get; set; }
        public string Inizio { get; set; }
        public string Fine { get; set; }
        public string Descrizione { get; set; }
    }
}