using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


using MySql.Data.MySqlClient;

namespace PrawkoAndroid
{
    public class ConnectionString
    {
        public string Serwer;
        public string Port;
        public string Uzytkownik;
        public string Haslo;
        public string BazaDanych;
        public string Typ;

        public ConnectionString(string s, string p, string u, string h, string b, string bd)
        {
            Serwer = s;
            Port = p;
            Uzytkownik = u;
            Haslo = h;
            BazaDanych = b;
            Typ = bd;
        }

        public override string ToString()
        {

            string s = "";

            if (Typ == "MsSql")
            {
                s = "Server=" + Serwer + "," + Port + ";Database=" + BazaDanych + ";User Id=" + Uzytkownik + ";Password = " + Haslo + "; ";
            }
            if (Typ == "MySql")
            {
                s = "Server = " + Serwer + "; Port = " + Port + "; Database = " + BazaDanych + "; Uid = " + Uzytkownik + "; Pwd = " + Haslo + ";";
            }

            return s;
        }
    }

    class SQLquick
    {
  
        private MySqlConnection MyPolaczenie;
        public ConnectionString CS;

        public SQLquick(ConnectionString cs)
        {
            CS = cs;
            Initialize();
        }
        private void Initialize()
        {
            
                MyPolaczenie = new MySqlConnection();

                MyPolaczenie.ConnectionString = CS.ToString();
            
        }
        public bool TestPolaczenia()
        {
   
             try
                {
                    MyPolaczenie.Open();

                    return true;
                }
                catch (MySqlException ex)
                {

                    Console.WriteLine("Cannot connect to server.  Contact administrator" + ex.Message);
                    return false;
                }
            
        }

        //open connection to database
        private bool OpenConnection()
        {
           
           
                try
                {
                    MyPolaczenie.Open();

                    return true;
                }
                catch (MySqlException ex)
                {

                    Console.WriteLine("Cannot connect to server.  Contact administrator");
                    return false;
                }
           
        }

        //Close connection
        public bool CloseConnection()
        {
          
                try
                {

                    MyPolaczenie.Close();
                    return true;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);

                    return false;
                }
          
        }

        //Insert statement
        public void Insert(Pytanie p)
        {
           
                string query = "INSERT INTO pytanie (Nazwa_Pytania,Numer_Pytania,TrescPL,OdpApl,OdpBpl,OdpCpl,Media,Zakres_Struktury,LiczbaPunktow,Kategorie,NazwaBloku,ZrodloPytania,Sens,Bezpieczenstwo)" +
                " VALUES('" +p.Nazwa_Pytania + "','"+p.Numer_Pytania+"','"+p.TrescPL + "','" +p.OdpApl + "','" +p.OdpBpl + "','" +p.OdpCpl + "','" +
                p.Media + "','" +p.Zakres_Struktury + "','" +p.LiczbaPunktow + "','" +p.Kategorie + "','" +p.NazwaBloku + "','" + p.ZrodloPytania + "','" +
                p.Sens + "','" +p.Bezpieczenstwo + "')" ;

                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, MyPolaczenie);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                }
           
        }
        public MySqlDataReader ExCom(MySqlCommand cmd) { this.OpenConnection(); MySqlDataReader reader = cmd.ExecuteReader();return reader; }
    }
}