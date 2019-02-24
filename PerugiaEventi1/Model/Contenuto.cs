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
    public class Contenuto
    {
        public string Id_contenuto { get; set; }
        public List<object> Id_contenuti_relazionati { get; set; }
        public string Titolo { get; set; }
        public string Url_risorsa { get; set; }
        public string Descrizione { get; set; }
        public string Data_inizio { get; set; }
        public string Data_fine { get; set; }
        public string Keywords { get; set; }
        public string Titolo_testo { get; set; }
        public List<Descrizioni> Descrizioni { get; set; }
        public List<object> Categorie_associate { get; set; }
        public List<Immagini> Immagini { get; set; }
        public List<object> Link_esterni_associati { get; set; }
        public string Immagine_spalla_destra { get; set; }
        public string Immagine_copertina { get; set; }
        public string Testo_alternativo_immagine_copertina { get; set; }
        public Coordinate Coordinate { get; set; }
        public string Comune { get; set; }
        public string Codice_istat_comune { get; set; }
        public List<AltriIndirizzi> Altri_indirizzi { get; set; }
        public string @abstract { get; set; }
        public string Ora_inizio { get; set; }
        public string Ora_fine { get; set; }
        public string Indirizzo { get; set; }
        public string Localita { get; set; }
        public string Cap { get; set; }
        public string Kml { get; set; }
    }
}