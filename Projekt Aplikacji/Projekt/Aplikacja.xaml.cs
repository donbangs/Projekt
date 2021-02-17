using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Aplikacja.xaml
    /// </summary>
    public partial class Aplikacja : Window
    {
     
        MySqlCommand komenda;
        MySqlDataReader czytnik;
        MySqlConnection pol = new MySqlConnection("server=localhost;user=root;database=kalorycznosc");
        string zapytanieSQL;
        Osoba osoba;
        public ObservableCollection<Produkty> produkty { get; set; }
        Produkty produkt;
        double bmmr;
        string dzien;
        double tluszcz;
        double weglowadany;
        double kalorie;
        double bialko;
        double zakresA = 0;
        double zakresB = 0;
        double wagaIdealna=0;
        
       
        public Aplikacja(Osoba osoba)
        {
            produkty = new ObservableCollection<Produkty>();
            InitializeComponent();
            DataContext = this;
            this.osoba = osoba;
            zakresA = Math.Round(18.5 * Math.Pow(osoba.Wzrost/100, 2),2);
            zakresB = Math.Round(25 * Math.Pow(osoba.Wzrost / 100, 2), 2);
            Zakres.Content = "Zakres: " + zakresA+"-"+zakresB;
            if (osoba.Waga > zakresB)
            {
                Prawidłowa.Content = "Nadwaga";
            }
            else if (osoba.Waga < zakresA)
            {
                Prawidłowa.Content = "Niedowaga";
            }
            else {
                Prawidłowa.Content = "Waga prawidłowa";
            }
            if (osoba.Plec == "K")
            {
                wagaIdealna = osoba.Wzrost - 100 - (osoba.Wzrost - 150) / 2.5;
            }
            else{
                wagaIdealna = osoba.Wzrost - 100 - (osoba.Wzrost - 150) / 4;

            }
           
            WagaIdealna.Content = "Waga Idealna: "+wagaIdealna;
      
            if (osoba.Plec == "k")
            {
                bmmr = ((9.99 * (double)osoba.Waga) + (6.25 * (double)osoba.Wzrost) - (4.92 * (double)osoba.Wiek) + 5);
                bmr.Content = "Bmr: " + bmmr.ToString();
            }
            else
            {
                bmmr = ((9.99 * (double)osoba.Waga) + (6.25 * (double)osoba.Wzrost) - (4.92 * (double)osoba.Wiek) - 161);
                bmr.Content = "Bmr: " + bmmr.ToString();
            }

            dzien = DateTime.Today.ToString("d");

            if (pol.State == ConnectionState.Closed)
            {
                pol.Open();
                zapytanieSQL = "select * from produkty";
                komenda = new MySqlCommand(zapytanieSQL, pol);
                czytnik = komenda.ExecuteReader();

                if (czytnik.HasRows)
                {
                    while (czytnik.Read())
                    {
                        produkt = new Produkty((int)czytnik["id"], czytnik["nazwa"].ToString(), (double)czytnik["kcal"], (double)czytnik["bialko"], (double)czytnik["weglowodany"], (double)czytnik["tluszcz"]);
                        produkty.Add(produkt);
                    }
                    czytnik.Close();
                }
            }
            pol.Close();

            if (pol.State == ConnectionState.Closed)
            {
                pol = new MySqlConnection("server=localhost;user=root;database=spozytekalorie");
                pol.Open();
                zapytanieSQL = "select * from " + osoba.Login;
                komenda = new MySqlCommand(zapytanieSQL, pol);
                czytnik = komenda.ExecuteReader();

                if (czytnik.HasRows)
                {
                    while (czytnik.Read())
                    {
                        if (dzien == czytnik["dzien"].ToString())
                        {
                            produkt = produkty[(int)czytnik["idProduktu"] - 1];
                            produkt.ilosc = double.Parse(czytnik["iloscProduktu"].ToString());
                            kalorie += ((int)czytnik["iloscProduktu"] / 100) * (produkt.kcal);
                            weglowadany += ((int)czytnik["iloscProduktu"] / 100) * (produkt.weglowadany);
                            bialko += ((int)czytnik["iloscProduktu"] / 100) * (produkt.bialko);
                            tluszcz += ((int)czytnik["iloscProduktu"] / 100) * (produkt.tluszcz);
                            lb3.Items.Add(produkt);
                            Bilans.Content = " Bilans " + (bmmr - kalorie);
                        }
                    }
                    czytnik.Close();
                }
            }
            pol.Close();
            produkty = new ObservableCollection<Produkty>(produkty.OrderBy(i => i.nazwa));
        
            bmi.Content = "Bmi: " + Math.Round((osoba.Waga / ((osoba.Wzrost / 100) * (osoba.Wzrost / 100))),2);
            wg.Content = "Węglowodany " + weglowadany.ToString();
            tl.Content = "Tłuszcz " + tluszcz.ToString();
            kcal.Content = "Kcal " + kalorie.ToString();
            bk.Content = "Białko " + bialko.ToString();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lb1.SelectedItem != null)
            {
                produkt = (Produkty)lb1.SelectedItem;
                kalorie += (double.Parse(gramy.Text)/100) * (produkt.kcal);
                lb3.Items.Add(produkt);
                Bilans.Content =" Bilans " +(bmmr - kalorie);
                weglowadany += (double.Parse(gramy.Text) / 100) * (produkt.weglowadany);
                bialko += (double.Parse(gramy.Text) / 100) * (produkt.bialko);
                tluszcz += (double.Parse(gramy.Text) / 100) * (produkt.tluszcz);
                wg.Content = "Węglowodany " + weglowadany.ToString();
                tl.Content = "Tłuszcz " + tluszcz.ToString();
                kcal.Content = "Kcal " + kalorie.ToString();
                bk.Content = "Białko " + bialko.ToString();
            }
            else if (lb2.SelectedItem != null)
            {
                produkt = (Produkty)lb2.SelectedItem;
                kalorie += (double.Parse(gramy.Text) / 100) * (produkt.kcal);
                lb3.Items.Add(produkt);
                Bilans.Content = " Bilans " + (bmmr - kalorie);
                weglowadany += (double.Parse(gramy.Text) / 100) * (produkt.weglowadany);
                bialko += (double.Parse(gramy.Text) / 100) * (produkt.bialko);
                tluszcz += (double.Parse(gramy.Text) / 100) * (produkt.tluszcz);
                wg.Content = "Węglowodany " + weglowadany.ToString();
                tl.Content = "Tłuszcz " + tluszcz.ToString();
                kcal.Content = "Kcal " + kalorie.ToString();
                bk.Content = "Białko " + bialko.ToString();
            }
            produkt.ilosc = double.Parse(gramy.Text);

            if (pol.State == ConnectionState.Closed)
            {
                pol = new MySqlConnection("server=localhost;user=root;database=spozytekalorie");
                zapytanieSQL = "insert into " + osoba.Login + " values('" + dzien + "','" + produkt.id + "','" + double.Parse(gramy.Text)+ "')";
                pol.Open();
                komenda = new MySqlCommand(zapytanieSQL, pol);
                czytnik = komenda.ExecuteReader();
                czytnik.Close();
                pol.Close();
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           lb2.Items.Clear();
           var l = produkty.Where(n => n.nazwa.Contains(Wyszukaj.Text));
            
            foreach (Produkty produkt in l)
            {
                lb2.Items.Add(produkt);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DodawanieProduktow dodawanie = new DodawanieProduktow(osoba);
            dodawanie.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Konto konto = new Konto(osoba);
            konto.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HistoriaSpozycia historia = new HistoriaSpozycia(osoba,produkty);
            historia.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Process.Start(@"D:\Laptop\Seminaria\Projekt\index.html");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Spalanie spalanie = new Spalanie(kalorie);
            spalanie.Show();
        }
    }
}
