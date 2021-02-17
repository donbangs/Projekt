using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekt
{
    public class Osoba
    {
        public string Login{ get; set; }
        public PasswordBox haslo;
        public double Waga { get; set; }
        public double Wzrost { get; set; }
        public string Plec { get; set; }
        public double Wiek { get; set; }
        public int AktywnoscFizyczna { get; set; }


        public Osoba(string login, PasswordBox password)
        {
            this.Login = login;
            this.haslo = password;
        }

        public void dane(string weight,string height,string sex, string age,int physical)
        {
            this.Waga = double.Parse(weight);
            this.Wzrost = double.Parse(height);
            this.Plec = sex;
            this.Wiek = double.Parse(age);
            AktywnoscFizyczna = physical;
        }


    }
}
