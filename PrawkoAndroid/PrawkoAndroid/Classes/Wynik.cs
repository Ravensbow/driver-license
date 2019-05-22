using System;
using System.Collections.Generic;
using System.Text;

namespace PrawkoAndroid.Classes
{
    public class Wynik
    {
        public List<string> WybraneOdpowiedzi { get; set; }
        public List<Pytanie> WylosowanePytania { get; set; }
        public int ZdobytePunkty { get; set; }

        public Wynik() { }
        public Wynik(List<string> wo,List<Pytanie> pyta, int points)
        {
            WybraneOdpowiedzi = wo;
            WylosowanePytania = pyta;
            ZdobytePunkty = points;
        }
    }
}
