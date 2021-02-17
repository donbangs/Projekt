using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Spalanie.xaml
    /// </summary>
    public partial class Spalanie : Window
    {
        double kalorie;
        MySqlCommand komenda;
        MySqlDataReader czytnik;
        MySqlConnection pol = new MySqlConnection("server=localhost;user=root;database=spalaniekalorii");
        string zapytanieSQL;
        CzasSpalania czas;
        List<CzasSpalania> lista = new List<CzasSpalania>();
        public Spalanie(double kalorie)
        {
            this.kalorie = kalorie;
            InitializeComponent();
            Spalone.Items.Add("Liczba Kalori do spalenia "+ kalorie);
            if (pol.State == ConnectionState.Closed)
            {
                pol.Open();
                zapytanieSQL = "select * from spalanie";
                komenda = new MySqlCommand(zapytanieSQL, pol);
                czytnik = komenda.ExecuteReader();

                if (czytnik.HasRows)
                {
                    while (czytnik.Read())
                    {
                        czas = new CzasSpalania(czytnik["nazwa"].ToString(),(double)czytnik["ilosc"]);
                        lista.Add(czas);
                        Spalone.Items.Add(czas.Czas(kalorie));
                    }
                    czytnik.Close();
                }
            }
            pol.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Spalone.Items.Clear();
            kalorie = double.Parse(Ilosc.Text);
            Spalone.Items.Add("Liczba Kalori do spalenia " + kalorie);
          
            foreach (CzasSpalania czas in lista)
            {
                Spalone.Items.Add(czas.Czas(kalorie));
            }

        }
    }
}
