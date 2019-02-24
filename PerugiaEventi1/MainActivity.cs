using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Net

namespace PerugiaEventi1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button bottoneCaricaEventi;
        ListView listaEventi;

        Uri uriEventi = http://dati.umbria.it/dataset/410faa97-546b-4362-a6d7-f8794d18ed19/resource/8afe729a-0f59-4647-95ee-481577e83bea/download/eventijsonitit.zipeventiitit.json;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            bottoneCaricaEventi = FindViewById<Button>(Resource.Id.bottoneCaricaEventi);
            listaEventi = FindViewById<ListView>(Resource.Id.listaEventi);

            manager = DownloadManager.FromContext(this);

             request = new DownloadManager.Request(Uri.Parse(uri));
            long downloadId = manager.Enqueue(request);
        }
    }
}