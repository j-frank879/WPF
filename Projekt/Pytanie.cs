using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Pytanie
    {
        public String Tresc { get; set; }
        public List<Odpowiedz> Odpowiedzi { get; set; }
        public Pytanie() { Tresc = "test"; Odpowiedzi = new List<Odpowiedz>(); }
        public Pytanie(String tresc,List<Odpowiedz> odpowiedzi)
        {
                Tresc = tresc;
            Odpowiedzi = odpowiedzi;

        }
       public  Pytanie(String tresc)
        {
            Tresc = tresc;
            Odpowiedzi = new List<Odpowiedz>();

        }
        

    }
}
