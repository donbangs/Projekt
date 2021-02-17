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
using MySql.Data.MySqlClient;
namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy DodawanieProduktow.xaml
    /// </summary>
    public partial class DodawanieProduktow : Window
    {
        MySqlCommand komenda;
        MySqlDataReader czytnik;
        MySqlConnection pol = new MySqlConnection("server=localhost;user=root;database=kalorycznosc");
        string zapytanieSQL;
        Osoba osoba;
        public DodawanieProduktow(Osoba osoba)
        {
            this.osoba = osoba;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pol.State == ConnectionState.Closed)
            {
                pol.Open();
                zapytanieSQL = "insert into produkty values('" + "null" + "','" + Nazwa.Text + "','" + Kalorie.Text + "','" + Białko.Text + "','" + Węglowodany.Text + "','" + Tłuszcz.Text+"')";
                komenda = new MySqlCommand(zapytanieSQL, pol);
                czytnik = komenda.ExecuteReader();
                czytnik.Close();
            }
            pol.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Aplikacja apk = new Aplikacja(osoba);
            apk.Show();
            this.Close();

        }
    }
}
