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
    /// Interaction logic for Konto.xaml
    /// </summary>
    public partial class Konto : Window
    {
        Osoba osoba;
        int aktywnosc;
        string plec;
        MySqlCommand komenda;
        MySqlDataReader czytnik;
        MySqlConnection pol = new MySqlConnection("server=localhost;user=root;database=dane");
        string zapytanieSQL = "";
        public Konto(Osoba osoba)
        {
            InitializeComponent();
            this.osoba = osoba;
            comboBox1.Items.Add("Brak aktywnosci fizycznej");
            comboBox1.Items.Add("Mała aktywnosc fizyczna(cwiczenia 1-3 tygodniowo)");
            comboBox1.Items.Add("Srednia aktywnosc fizyczna(cwiczenia 3-5 tygodniowo)");
            comboBox1.Items.Add("Duża aktywnosc fizyczna(cwiczenia codzienne)");
            comboBox1.Items.Add("Bardzo duża aktywnosc fizyczna");
            textbox1.Text = osoba.Waga.ToString();
            textbox2.Text = osoba.Wzrost.ToString();
            textbox4.Text = osoba.Wiek.ToString();
            comboBox1.SelectedItem = osoba.AktywnoscFizyczna;
            if (osoba.Plec=="K")
            {
                K.IsChecked = true;
            }
            else
            {
                M.IsChecked = true;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aktywnosc = comboBox1.SelectedIndex;
        }

        private void M_Checked(object sender, RoutedEventArgs e)
        {
            plec = "M";
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            plec = "K";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pol = new MySqlConnection("server=localhost;user=root;database=dane");
            if (pol.State == ConnectionState.Closed)
            {
                pol.Open();
                zapytanieSQL = "update dane set waga  = '" + textbox1.Text + "',wzrost='" + textbox2.Text + "',plec='" + plec + "',wiek='" + textbox4.Text + "',aktywnosc='" + aktywnosc + "' where login = '" + osoba.Login+"'" ;
                komenda = new MySqlCommand(zapytanieSQL, pol);
                czytnik = komenda.ExecuteReader();
             
            }
             osoba.dane(textbox1.Text, textbox2.Text, plec, textbox4.Text, aktywnosc);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Aplikacja aplikacja = new Aplikacja(osoba);
            aplikacja.Show();
            this.Close();
        }
    }
}
