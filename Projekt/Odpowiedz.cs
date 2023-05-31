using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
   public class Odpowiedz
    { public Odpowiedz() { tresc = "odpowiedz";poprawnosc = true; }
        public Odpowiedz(String tresc, bool poprawnosc)
        {
            this.tresc = tresc;
            this.poprawnosc = poprawnosc;
        }
        private String tresc { get; set; }
        private bool poprawnosc { get; set; }

    }
}
