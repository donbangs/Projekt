using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class CzasSpalania
    {
        string nazwa;
        double ilosc;
        public CzasSpalania(string nazwa,double ilosc)
        {
            this.nazwa = nazwa;
            this.ilosc = ilosc;
        }
        public string Czas(double kalorie)
        {
            double czas = Math.Round(kalorie / ilosc,2);

            return czas.ToString()+"h "+nazwa;

        }
    }
}
