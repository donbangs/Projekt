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
    /// Interaction logic for tworzenieKonta.xaml
    /// </summary>
    public partial class tworzenieKonta : Window
    {
        Osoba osoba;
        int aktywnosc;
        string plec;
        public tworzenieKonta(Osoba osoba)
        {
            InitializeComponent();
            this.osoba = osoba;    
            comboBox1.Items.Add("Brak aktywności fizycznej");
            comboBox1.Items.Add("Mała aktywność fizyczna(cwiczenia 1-3 tygodniowo)");
            comboBox1.Items.Add("Srednia aktywność fizyczna(cwiczenia 3-5 tygodniowo)");
            comboBox1.Items.Add("Duża aktywność fizyczna(cwiczenia codzienne)");
            comboBox1.Items.Add("Bardzo duża aktywność fizyczna");

        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aktywnosc = comboBox1.SelectedIndex;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            osoba.dane(textbox1.Text, textbox2.Text, plec, textbox4.Text, aktywnosc);

            MySqlCommand komenda;
            MySqlDataReader czytnik;
            MySqlConnection pol = new MySqlConnection("server=localhost;user=root;database=dane");
            string zapytanieSQL;
            if (pol.State == ConnectionState.Closed)
            {
                pol.Open();
                zapytanieSQL = "insert into dane values('" + osoba.Login + "','" + osoba.Waga + "','" + osoba.Wzrost + "','" + osoba.Plec + "','" + osoba.Wiek + "','" + aktywnosc + "')";
                komenda = new MySqlCommand(zapytanieSQL, pol);
                czytnik = komenda.ExecuteReader();
                czytnik.Close();
                pol.Close();
            }

         

         
            if (pol.State == ConnectionState.Closed)
            {
                pol = new MySqlConnection("server=localhost;user=root;database=spozytekalorie");
                zapytanieSQL = "create table " + osoba.Login + " (dzien varchar(100), idProduktu int, iloscProduktu int);";
                pol.Open();
                komenda = new MySqlCommand(zapytanieSQL, pol);
                czytnik = komenda.ExecuteReader();
                czytnik.Close();
                pol.Close();
            }

            Aplikacja aplikacja = new Aplikacja(osoba);
            this.Close();
            aplikacja.Show();



        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            plec ="K";
        }

        private void M_Checked(object sender, RoutedEventArgs e)
        {
            plec = "M";
        }
    }
}
