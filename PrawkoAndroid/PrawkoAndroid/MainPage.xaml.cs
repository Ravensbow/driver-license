using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrawkoAndroid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
            dodawanieKlil();
        }
       

        private void dodawanieKlil()
        {
            Command egzaminCommand = new Command(async o => {
                string op = await DisplayActionSheet("Wybierz Kategorie", "Anuluj", null, new string[] { "A", "B", "C", "D", "T" });
                if (op != "Anuluj" && op != "" && op != string.Empty && op != null) await Navigation.PushAsync(new PageEgzamin(op[0]));
            });
            stackEgzamin.GestureRecognizers.Add(new TapGestureRecognizer { Command = egzaminCommand });

        }
    }
}
