using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection;
using Plugin.MediaManager;
using System.IO;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;

namespace PrawkoAndroid
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageEgzamin : ContentPage
	{
        

        public Pytanie aktualnePytanie;
        public string aktualnaOdpowiedz;
        public List<Pytanie> Wylosowanee= new List<Pytanie>();
        public int zdobytePunkty = 0;
        public List<string> WybraneOdp = new List<string>();

        public string kategoria= "http://prawojazdy.gear.host/api/values/";
        public string imagePath = "PrawkoAndroid.Droid.Image.";
        public string wmvPath = "http://ravenbow.freeasphost.net/video/";

        public int index=0;

		public PageEgzamin (int kategoriaNr)
		{
            kategoria += kategoriaNr.ToString();
			InitializeComponent ();
            CrossMediaManager.Current.StatusChanged += OnVideoChange;
            dodawanieKlil();
		}
        protected override async void OnAppearing()
        {
            zdobytePunkty = 0;
            aktualnaOdpowiedz = null;
            Wylosowanee = await LosowaniePytan(kategoria);
            aktualnePytanie = Wylosowanee[0];
            index = 0;
            Zbuduj();
        }


        private void dodawanieKlil()
        {
            Command odpowiedzACommand = new Command(()=> odpAClicked());
            stackA.GestureRecognizers.Add(new TapGestureRecognizer { Command = odpowiedzACommand });
            Command odpowiedzBCommand = new Command(() => odpBClicked());
            stackB.GestureRecognizers.Add(new TapGestureRecognizer { Command = odpowiedzBCommand });
            Command odpowiedzCCommand = new Command(() => odpCClicked());
            stackC.GestureRecognizers.Add(new TapGestureRecognizer { Command = odpowiedzCCommand });
        }

        void odpAClicked()
        {
            if (labelOdpA.Text == "Tak") aktualnaOdpowiedz = "T";
            else if (labelOdpA.Text == "Nie") aktualnaOdpowiedz = "N";
            else aktualnaOdpowiedz = "A";
            stackA.BackgroundColor = Color.Tan;
            stackB.BackgroundColor = Color.Bisque;
            stackC.BackgroundColor = Color.Bisque;
        }
        void odpBClicked()
        {
            if (labelOdpB.Text == "Tak") aktualnaOdpowiedz = "T";
            else if (labelOdpB.Text == "Nie") aktualnaOdpowiedz = "N";
            else aktualnaOdpowiedz = "B";
            stackB.BackgroundColor = Color.Tan;
            stackA.BackgroundColor = Color.Bisque;
            stackC.BackgroundColor = Color.Bisque;
        }
        void odpCClicked()
        {
            if (labelOdpC.Text == "Tak") aktualnaOdpowiedz = "T";
            else if (labelOdpC.Text == "Nie") aktualnaOdpowiedz = "N";
            else aktualnaOdpowiedz = "C";
            stackC.BackgroundColor = Color.Tan;
            stackB.BackgroundColor = Color.Bisque;
            stackA.BackgroundColor = Color.Bisque;
        }

        void Zbuduj()
        {
            stackA.BackgroundColor = Color.Bisque;
            stackB.BackgroundColor = Color.Bisque;
            stackC.BackgroundColor = Color.Bisque;

            aktualnePytanie = Wylosowanee[index];

            labelTresc.Text = aktualnePytanie.TrescPL;
            indexLabel.Text = (index+1).ToString()+"/" + "32";
            nrPytLabel.Text = "nr. pytania: "+aktualnePytanie.Media;

            if (index < 20) zegarLabel.Text = "35";
            else zegarLabel.Text = "50";

            //Set Media
            pytanieVideo.IsVisible = false;
            pytanieImage.IsVisible = false;
            if ((aktualnePytanie.Media != "" || aktualnePytanie.Media != null || aktualnePytanie.Media != string.Empty) && aktualnePytanie.Media.Contains(".jpg"))
            {
                pytanieImage.IsVisible = true;
                pytanieImage.Source = ImageSource.FromResource(imagePath + aktualnePytanie.Media);
                Tajmer();
            }
            else if ((aktualnePytanie.Media != "" || aktualnePytanie.Media != null || aktualnePytanie.Media != string.Empty) && aktualnePytanie.Media.Contains(".wmv"))
            {
                pytanieVideo.IsVisible = true;


                CrossMediaManager.Current.Play(wmvPath+aktualnePytanie.Media, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Video);
                //Tajmer();
            }
            else
            {
                pytanieImage.IsVisible = true;
                pytanieImage.Source = ImageSource.FromResource(imagePath + "test.png");
                Tajmer();
            }
            //Set Answer Button Text
            if (aktualnePytanie.OdpApl==""|| aktualnePytanie.OdpApl==null)
            {
                labelOdpA.Text = "Tak";
                labelOdpB.Text = "Nie";
                stackC.IsVisible = false;
            }
            else
            {
                stackC.IsVisible = true;
                labelOdpA.Text = aktualnePytanie.OdpApl;
                labelOdpB.Text = aktualnePytanie.OdpBpl;
                labelOdpC.Text = aktualnePytanie.OdpCpl;
            }
            //Next
            index++;
        }

        void OnZatwierdz(object sender, EventArgs e)
        {
            WybraneOdp.Add(aktualnaOdpowiedz);

            if (aktualnePytanie.PoprawnaOdp == "T" && aktualnaOdpowiedz == "Tak") zdobytePunkty += Convert.ToInt32(aktualnePytanie.LiczbaPunktow);
            else if (aktualnePytanie.PoprawnaOdp == "N" && aktualnaOdpowiedz == "Nie") zdobytePunkty += Convert.ToInt32(aktualnePytanie.LiczbaPunktow);
            else if (aktualnaOdpowiedz == aktualnePytanie.PoprawnaOdp) zdobytePunkty += Convert.ToInt32(aktualnePytanie.LiczbaPunktow);

            if (index < Wylosowanee.Count)
            {
                aktualnaOdpowiedz = null;
                Zbuduj();
            }
            else
            {
                aktualnaOdpowiedz = null;
                Navigation.PushAsync(new PageWynik(new Classes.Wynik(WybraneOdp, Wylosowanee, zdobytePunkty)));
            }

        }

        async static Task<List<Pytanie>> LosowaniePytan(string url)
        {
           
            List<Pytanie> wylosowane = new List<Pytanie>();
            using (HttpClient webClient = new HttpClient())
            {
       
                using (HttpResponseMessage response = await webClient.GetAsync(url))
                {
                   
                    using (HttpContent content = response.Content)
                    {
                        string strContent = await content.ReadAsStringAsync();
                        wylosowane = JsonConvert.DeserializeObject<List<Pytanie>>(strContent);
                    }
                }
            }
            return wylosowane;
        }

        void OnVideoChange(object sender,EventArgs e)
        {
            if(CrossMediaManager.Current.Status ==MediaPlayerStatus.Stopped)
            {
                
            }
            if (CrossMediaManager.Current.Status == MediaPlayerStatus.Playing)
            {
                Tajmer();
            }
        }

        void Tajmer()
        {
            string nw = aktualnePytanie.Numer_Pytania;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                
                
                int a = Convert.ToInt32(zegarLabel.Text);
                a--;
                
                if (nw != aktualnePytanie.Numer_Pytania) {zegarLabel.TextColor = Color.Gray; return false; }
                zegarLabel.Text = a.ToString();
                if (a < 10) zegarLabel.TextColor = Color.Red;
                if (a > 0) return true;
                else
                {
                    WybraneOdp.Add(aktualnaOdpowiedz);
                    zegarLabel.TextColor = Color.Gray;
                    if (aktualnePytanie.PoprawnaOdp == "T" && aktualnaOdpowiedz == "Tak") zdobytePunkty += Convert.ToInt32(aktualnePytanie.LiczbaPunktow);
                    else if (aktualnePytanie.PoprawnaOdp == "N" && aktualnaOdpowiedz == "Nie") zdobytePunkty += Convert.ToInt32(aktualnePytanie.LiczbaPunktow);
                    else if (aktualnaOdpowiedz == aktualnePytanie.PoprawnaOdp) zdobytePunkty += Convert.ToInt32(aktualnePytanie.LiczbaPunktow);

                    if (index < Wylosowanee.Count)
                    {
                        Zbuduj();
                    }
                    else
                    {
                        Navigation.PushAsync(new PageWynik(new Classes.Wynik(WybraneOdp, Wylosowanee, zdobytePunkty)));
                    }
                    return false;
                }
            });
        }
    }
}