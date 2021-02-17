using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.Diagnostics;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        MySqlCommand komenda;
        MySqlDataReader czytnik;
        MySqlConnection pol = new MySqlConnection("server=localhost;user=root;database=loginsandpasswords");
        string zapytanieSQL = "";
        Osoba osoba;
        double utworz;
        public static int HashNormal(string text)
        {
            return text.GetHashCode();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

                if (brakKonta.IsChecked == true)
                {
                if (password.Password == password2.Password)
                {
                    if (pol.State == ConnectionState.Closed)
                    {
                        pol.Open();
                        utworz = 0;
                        zapytanieSQL = "select * from dane";
                        komenda = new MySqlCommand(zapytanieSQL, pol);
                        czytnik = komenda.ExecuteReader();

                        if (czytnik.HasRows)
                        {
                            while (czytnik.Read())
                            {
                                if (texbox1.Text == czytnik["login"].ToString())
                                {
                                    utworz = 1;
                                    MessageBox.Show("Login o takiej nazwie już istnieje");
                                    break;
                                }
                            }
                            czytnik.Close();
                        }
                    }
                    pol.Close();

                    if (utworz == 0)
                    {
                        if (pol.State == ConnectionState.Closed)
                        {
                            pol.Open();
                            osoba = new Osoba(texbox1.Text, password);
                            zapytanieSQL = "insert into dane values('" + HashNormal(osoba.Login) + "','" + HashNormal(osoba.haslo.Password.ToString()) + "')";
                            komenda = new MySqlCommand(zapytanieSQL, pol);
                            czytnik = komenda.ExecuteReader();
                            czytnik.Close();
                            pol.Close();
                        }
                        tworzenieKonta tworzenieKonta = new tworzenieKonta(osoba);
                        tworzenieKonta.Show();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Hasła nie są takie same ");
                }
            }
            if (konto.IsChecked == true)
            {
                int x = 0;
                if (pol.State == ConnectionState.Closed)
                {
                    pol.Open();
                    zapytanieSQL = "select * from dane";
                    komenda = new MySqlCommand(zapytanieSQL, pol);
                    czytnik = komenda.ExecuteReader();

                    if (czytnik.HasRows)
                    {
                        while (czytnik.Read())
                        {
                            if (HashNormal(texbox1.Text).ToString()== czytnik["login"].ToString() && HashNormal(password.Password.ToString()).ToString() == czytnik["haslo"].ToString())
                            {
                                osoba = new Osoba(texbox1.Text, password);
                                MessageBox.Show("Poprawne Logowanie");
                                x = 1;
                            }  
                        }
                        czytnik.Close();
                    }
                }
                pol.Close();
                if (x == 1)
                {
                    pol = new MySqlConnection("server=localhost;user=root;database=dane");
                    if (pol.State == ConnectionState.Closed)
                    { 
                        pol.Open();
                        zapytanieSQL = "select * from dane where login = '"+osoba.Login+"'";
                        komenda = new MySqlCommand(zapytanieSQL, pol);
                        czytnik = komenda.ExecuteReader();
                        while (czytnik.Read())
                        {
                            osoba.dane(czytnik["waga"].ToString(), czytnik["wzrost"].ToString(), czytnik["plec"].ToString(), czytnik["wiek"].ToString(), (int)czytnik["aktywnosc"]);
                        }
                    }
                  
                    Aplikacja aplikacja = new Aplikacja(osoba);
                    aplikacja.Show();
                    this.Close();

                }

                    if (x == 0)
                {
                    MessageBox.Show("Nie poprawny login lub hasło");
                }
            }
        }



        private void konto_Checked(object sender, RoutedEventArgs e)
        {
            password2.Visibility = Visibility.Collapsed;
            Potwierdz.Visibility = Visibility.Collapsed;
        }

        private void brakKonta_Checked(object sender, RoutedEventArgs e)
        {
            password2.Visibility = Visibility.Visible;
            Potwierdz.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start(@"D:\Laptop\Seminaria\Projekt\index.html"); 
        }
    }
}
