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
using Newtonsoft.Json;

namespace PerugiaEventi1.Model
{
    public class Contenuto
    {
        [JsonProperty("id-contenuto")]
        public string Id_contenuto { get; set; }
        public List<object> Id_contenuti_relazionati { get; set; }
        public string Titolo { get; set; }
        [JsonProperty("url-risorsa")]
        public string Url_risorsa { get; set; }
        public string Descrizione { get; set; }
        [JsonProperty("data-inizio")]
        public string Data_inizio { get; set; }
        [JsonProperty("data-fine")]
        public string Data_fine { get; set; }
        public string Keywords { get; set; }
        [JsonProperty("titolo-testo")]
        public string Titolo_testo { get; set; }
        public List<Descrizioni> Descrizioni { get; set; }
        [JsonProperty("categorie-associate")]
        public List<object> Categorie_associate { get; set; }
        public List<Immagini> Immagini { get; set; }
        [JsonProperty("link-esterni-associati")]
        public List<object> Link_esterni_associati { get; set; }
        [JsonProperty("immaggine-spalla-destra")]
        public string Immagine_spalla_destra { get; set; }
        [JsonProperty("immaggine-copertina")]
        public string Immagine_copertina { get; set; }
        public string Testo_alternativo_immagine_copertina { get; set; }
        public Coordinate Coordinate { get; set; }
        public string Comune { get; set; }
        public string Codice_istat_comune { get; set; }
        public List<AltriIndirizzi> Altri_indirizzi { get; set; }
        public string @abstract { get; set; }
        [JsonProperty("ora-inizio")]
        public string Ora_inizio { get; set; }
        [JsonProperty("ora-fine")]
        public string Ora_fine { get; set; }
        public string Indirizzo { get; set; }
        public string Localita { get; set; }
        public string Cap { get; set; }
        public string Kml { get; set; }
    }
}