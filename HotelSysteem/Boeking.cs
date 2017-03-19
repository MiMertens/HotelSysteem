using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSysteem
{
    class Boeking
    {
        
        public static bool bezet;
        public int kamerId { get; private set; }
        public string klantNaam { get; private set; }
        public string klantGeslacht { get; private set; }
        public string klantAdres { get; private set; }
        public int klantLeeftijd { get; private set; }
        public string boekingDatum { get; private set; }
        public string klantEten { get; private set; }

        public static List<Boeking> boekingen = new List<Boeking>();
        public List<Gasten> gasten = new List<Gasten>();

        public Boeking(int kamerid, string klantnaam, string klantgeslacht, string klantadres, int klantleeftijd, string boekingdatum, string klanteten )
        {
            kamerId = kamerid;
            klantNaam = klantnaam;
            klantGeslacht = klantgeslacht;
            klantAdres = klantadres;
            klantLeeftijd = klantleeftijd;
            boekingDatum = boekingdatum;
            klantEten = klanteten;
        }
    }
}
