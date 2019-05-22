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
	public partial class PageWynik : ContentPage
	{
        public PageWynik(Classes.Wynik wyniki)
        {
            InitializeComponent();
            
            labelPunktacja.Text = wyniki.ZdobytePunkty.ToString() + "/74";

            if (wyniki.ZdobytePunkty >= 68)
            {
                labelZdanie.Text = "Zdałeś!";
                //labelZdanie.TextColor = Color.DarkSeaGreen;
            }
            else
            {
                labelZdanie.Text = "Niezdałeś!";
                //labelZdanie.TextColor = Color.DarkRed;
            }

            StackLayout stack = new StackLayout { Orientation = StackOrientation.Vertical };
            int i = 0;
            foreach(Pytanie p in wyniki.WylosowanePytania)
            {
                Command odpowiedzACommand = new Command(() => AlertPytanie(p));
                BoxView bx = new BoxView();
                if (wyniki.WylosowanePytania[i].PoprawnaOdp == wyniki.WybraneOdpowiedzi[i])
                    bx.Color = Color.ForestGreen;
                else
                    bx.Color = Color.DarkRed;

                Label poprawna = new Label { TextColor = Color.Silver, FontSize = 18,Text="POPRAWNA: "+TekstOdp(wyniki.WylosowanePytania[i], wyniki.WylosowanePytania[i].PoprawnaOdp) };
                Label twoja = new Label {TextColor=Color.Silver, FontSize = 18, Text = "TWOJA: " + TekstOdp(wyniki.WylosowanePytania[i],wyniki.WybraneOdpowiedzi[i]) };

                StackLayout odp = new StackLayout { Orientation = StackOrientation.Vertical,Children = { poprawna,twoja} };
                StackLayout calosc = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { bx, odp } };
                
                calosc.GestureRecognizers.Add(new TapGestureRecognizer { Command = odpowiedzACommand });

                stack.Children.Add(calosc);
                i++;
            }
            scrollOdpowiedzi.Content = stack;
        }

        public string TekstOdp(Pytanie pyt,string odp)
        {
            string wynik="";
            if (odp == "A") wynik = pyt.OdpApl;
            else if (odp == "C") wynik = pyt.OdpCpl;
            else if (odp == "B") wynik = pyt.OdpBpl;
            else if (odp == "N") wynik = "Nie";
            else if (odp == "T") wynik = "Tak";
            return wynik;
        }

        public void AlertPytanie(Pytanie pyt)
        {
            DisplayAlert("Treść", pyt.TrescPL, "OK");
        }

	}
}