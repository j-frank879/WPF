using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Pytanie
    {
        Pytanie(String tresc,List<Odpowiedz> odpowiedzi)
        {
            this.tresc = tresc;
            this.odpowiedzi= odpowiedzi;

        }
        Pytanie(String tresc)
        {
            this.tresc = tresc;
            this.odpowiedzi = new List<Odpowiedz>();

        }
        private String tresc { get; set; }
        private List<Odpowiedz> odpowiedzi { get; set; }

        public void addOdpowiedz(Odpowiedz o) { odpowiedzi.Add(o); }
        public void clearOdpowiedzi(Odpowiedz o) { odpowiedzi.Clear(); }
    }
}
