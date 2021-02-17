using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Produkty
    {
        public string nazwa { get; set; }
        public int id;
        public double kcal { get; set; }
        public double bialko;
        public double weglowadany;
        public double tluszcz;
        public double ilosc;
        public string dzien { get; set; }

        public override string ToString()
        {
            return nazwa.ToString()+" ilość kcal w 100 g "+kcal.ToString()+" spozyto "+ilosc.ToString()+" gram";
        }
        public Produkty(int id, string nazwa, double kcal, double bialko, double weglowadany, double tluszcz)
        {
            this.id = id;
            this.nazwa = nazwa;
            this.kcal = kcal;
            this.bialko = bialko;
            this.weglowadany = weglowadany;
            this.tluszcz = tluszcz;
        }
    }
}
