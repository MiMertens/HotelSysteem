using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSysteem
{
    class Gasten
    {
        public int kamerId { get; private set; }
        public string gastNaam { get; private set; }
        public string gastGeslacht { get; private set; }
        public string gastAdres { get; private set; }
        public int gastLeeftijd { get; private set; }
        public string boekingDatum { get; private set; }
        public string gastEten { get; private set; }

        public Gasten(int kamerid, string gastnaam, string gastgeslacht, string gastadres, int gastleeftijd, string boekingdatum, string gasteten)
        {
            kamerId = kamerid;
            gastNaam = gastnaam;
            gastGeslacht = gastgeslacht;
            gastAdres = gastadres;
            gastLeeftijd = gastleeftijd;
            boekingDatum = boekingdatum;
            gastEten = gasteten;
        }

    }
}
