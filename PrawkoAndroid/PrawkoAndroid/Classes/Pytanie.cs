using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PrawkoAndroid
{
 
   
    public class Pytanie
    {
        
        public string Nazwa_Pytania { get; set; }
      
        public string Numer_Pytania { get; set; }



        //POLSKI
    
        public string TrescPL { get; set; }
     
        public string OdpApl { get; set; }
     
        public string OdpBpl { get; set; }
     
        public string OdpCpl { get; set; }


        //ANGIELSKI
       
        public string TrescEN { get; set; }
   
        public string OdpAen { get; set; }
    
        public string OdpBen { get; set; }

        public string OdpCen { get; set; }

        //NIEMIECKI

        public string TrescDE { get; set; }

        public string OdpAde { get; set; }

        public string OdpBde { get; set; }

        public string OdpCde { get; set; }


        public string PoprawnaOdp { get; set; }

        public string Media { get; set; }

        public string Zakres_Struktury { get; set; }

        public string LiczbaPunktow { get; set; }

        public string Kategorie { get; set; }

        public string NazwaBloku { get; set; }

        public string ZrodloPytania { get; set; }

        public string Sens { get; set; }

        public string Bezpieczenstwo { get; set; }

        public string Status { get; set; }

        public string Podmiot { get; set; }

        //MIGOWY

        public string MigPyt { get; set; }

        public string MigOdpA { get; set; }

        public string MigOdpB { get; set; }

        public string MigOdpC { get; set; }


        public Pytanie() { }
        public Pytanie(XmlDocument XmlDoc,int i)
        {
            Nazwa_Pytania = XmlDoc.GetElementsByTagName("Nazwa_x0020_pytania").Item(i).InnerText;
            Numer_Pytania = XmlDoc.GetElementsByTagName("Numer_x0020_pytania").Item(i).InnerText;
            TrescPL = XmlDoc.GetElementsByTagName("Pytanie").Item(i).InnerText;
            OdpApl = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_A").Item(i).InnerText;
            OdpBpl = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_B").Item(i).InnerText;
            OdpCpl = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_C").Item(i).InnerText;
            //TrescEN = XmlDoc.GetElementsByTagName("Pytanie_x0020_ENG").Item(i).InnerText;
            //OdpAen = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_ENG_x0020_A").Item(i).InnerText;
            //OdpBen = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_ENG_x0020_B").Item(i).InnerText;
            //OdpCen = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_ENG_x0020_C").Item(i).InnerText;
            //TrescDE = XmlDoc.GetElementsByTagName("Pytanie_x0020_DE").Item(i).InnerText;
            //OdpAde = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_DE_x0020_A").Item(i).InnerText;
            //OdpBde = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_DE_x0020_B").Item(i).InnerText;
            //OdpCde = XmlDoc.GetElementsByTagName("Odpowiedź_x0020_DE_x0020_C").Item(i).InnerText;
            PoprawnaOdp = XmlDoc.GetElementsByTagName("Poprawna_x0020_odp").Item(i).InnerText;
            Media = XmlDoc.GetElementsByTagName("Media").Item(i).InnerText;
            Zakres_Struktury = XmlDoc.GetElementsByTagName("Zakres_x0020_struktury").Item(i).InnerText;
            LiczbaPunktow = XmlDoc.GetElementsByTagName("Liczba_x0020_punktów").Item(i).InnerText;
            Kategorie = XmlDoc.GetElementsByTagName("Kategorie").Item(i).InnerText;
            NazwaBloku = XmlDoc.GetElementsByTagName("Nazwa_x0020_bloku").Item(i).InnerText;
            ZrodloPytania = XmlDoc.GetElementsByTagName("Źródło_x0020_pytania").Item(i).InnerText;
            Sens = XmlDoc.GetElementsByTagName("O_x0020_co_x0020_chcemy_x0020_zapytać").Item(i).InnerText;
            Bezpieczenstwo = XmlDoc.GetElementsByTagName("Jaki_x0020_ma_x0020_związek_x0020_z_x0020_bezpieczeństwem").Item(i).InnerText;
            //Status = XmlDoc.GetElementsByTagName("Status").Item(i).InnerText;
            //Podmiot = XmlDoc.GetElementsByTagName("Podmiot").Item(i).InnerText;
            //MigPyt = XmlDoc.GetElementsByTagName("Nazwa_x0020_media_x0020_tłumaczenie_x0020_migowe_x0020__x0028_PJM_x0029__x0020_treść_x0020_pyt").Item(i).InnerText;
            //MigOdpA = XmlDoc.GetElementsByTagName("Nazwa_x0020_media_x0020_tłumaczenie_x0020_migowe_x0020__x0028_PJM_x0029__x0020_treść_x0020_odp_x0020_A").Item(i).InnerText;
            //MigOdpB = XmlDoc.GetElementsByTagName("Nazwa_x0020_media_x0020_tłumaczenie_x0020_migowe_x0020__x0028_PJM_x0029__x0020_treść_x0020_odp_x0020_B").Item(i).InnerText;
            //MigOdpC = XmlDoc.GetElementsByTagName("Nazwa_x0020_media_x0020_tłumaczenie_x0020_migowe_x0020__x0028_PJM_x0029__x0020_treść_x0020_odp_x0020_C").Item(i).InnerText;
        }
    }
    class PasekLadowania
    {
        int stan=0;
        int max;
        int prog;
        int dlugosc;
        int przekroczone = 0;

        public PasekLadowania(int max,int dlugosc)
        {
            this.dlugosc = dlugosc;
            this.max = max;
            prog = max / dlugosc;
        }

        public void Wyswietl()
        {
            Aktualizuj();
            string pasek = "[";
            for(int a=0;a<dlugosc;a++)
            {
                if (a < przekroczone) pasek += "#";
                else pasek += " ";
            }
            pasek += "]";

            string liczby="";
            int b;
            if (((dlugosc / 2) - 6) >= 0)
                b = (dlugosc / 2) - 6;
            else
                b = 0;
            for (int a=0;a<b;a++)
            {
                liczby += " ";
            }
            liczby = stan.ToString() + "/" + max.ToString();

            Console.WriteLine(pasek);
            Console.WriteLine(liczby);
            
            
        }
        public void Aktualizuj()
        {
            stan++;
            if(stan>=prog)
            {
                przekroczone++;
                stan = 0;
            }

        }
    }

    

}
