using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy HistoriaSpozycia.xaml
    /// </summary>
    public partial class HistoriaSpozycia : Window
    {
        Produkty produkt;
        MySqlCommand komenda;
        MySqlDataReader czytnik;
        MySqlConnection pol = new MySqlConnection("server=localhost;user=root;database=loginsandpasswords");
        string zapytanieSQL = "";
        Osoba osoba;
        public ObservableCollection<Produkty> produkty;
        int x = 0;
        double tluszcz=0;
        double weglowadany=0;
        double kalorie=0;
        double bialko=0;

        public HistoriaSpozycia(Osoba osoba,ObservableCollection<Produkty> produkty )
        {
            string dzien2="";
            this.osoba = osoba;
            this.produkty = produkty;
            InitializeComponent();

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
                            produkt = produkty[(int)czytnik["idProduktu"] - 1];
                            produkt.dzien = czytnik["dzien"].ToString();
                        if (produkt.dzien != dzien2)
                        {
                            if (x == 0)
                            {
                                dzien2 = produkt.dzien;
                                Historia.Items.Add(dzien2);
                                x++;
                            }
                            else
                            {
                                Historia.Items.Add("Podsumownanie "+"spożyto "+kalorie+ " kalori "+weglowadany+" węglowodanów " +tluszcz+" tłuszczu "+bialko+" białka");
                                dzien2 = produkt.dzien;
                                Historia.Items.Add(dzien2);
                                kalorie = 0;
                                weglowadany = 0;
                                bialko = 0;
                                tluszcz = 0;
                            }
                        }
                        produkt.ilosc = double.Parse(czytnik["iloscProduktu"].ToString());
                        kalorie += ((int)czytnik["iloscProduktu"] / 100) * (produkt.kcal);
                        weglowadany += ((int)czytnik["iloscProduktu"] / 100) * (produkt.weglowadany);
                        bialko += ((int)czytnik["iloscProduktu"] / 100) * (produkt.bialko);
                        tluszcz += ((int)czytnik["iloscProduktu"] / 100) * (produkt.tluszcz);
                        Historia.Items.Add(produkt);
                    }
                    czytnik.Close();
                }
            }
            pol.Close();
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Aplikacja aplikacja = new Aplikacja(osoba);
            aplikacja.Show();
            this.Close();
        }
    }
}
