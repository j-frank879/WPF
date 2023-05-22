using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
   public class Odpowiedz
    { Odpowiedz(String tresc, Boolean poprawnosc)
        {
            this.tresc = tresc;
            this.poprawnosc = poprawnosc;
        }
        private String tresc { get; set; }
        private Boolean poprawnosc { get; set; }

    }
}
