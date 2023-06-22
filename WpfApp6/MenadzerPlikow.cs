using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6
{
    public class ImportPytan
    {
        public Collection<Pytanie> Import(Collection<Pytanie> pytania, string SciezkaDoPliku)
        {
            var lines = File.ReadLines(SciezkaDoPliku);
            //var lines = File.ReadLines(@"..\..\..\ZestawPytan.txt");
            //Collection<Pytanie> pytania = new Collection<Pytanie>();

            foreach (var line in lines)
            {
                //Collection<Odpowiedz> odpowiedzi = new Collection<Odpowiedz>();
                string[] element = line.Split(";;");

                Pytanie nowePytanie = new Pytanie(element[0]);

                for (int i = 1; i < element.Length; i = i + 2)
                {
                    Odpowiedz odpowiedz = new Odpowiedz(element[i], Convert.ToBoolean(element[i+1]));
                    nowePytanie.Odpowiedzi.Add(odpowiedz);
                }

                //Pytanie nowePytanie = new Pytanie(element[0], odpowiedzi);
                pytania.Add(nowePytanie);

            }

            return pytania;
        }
        public void Export(Collection<Pytanie> pytania, string SciezkaDoPliku)
        {
            List<Pytanie> pytaniaLista = pytania.ToList();
            List<string> pytaniaPoFormacie = new List<string>();
            pytaniaLista.ForEach(pytaniaLista => { pytaniaPoFormacie.Add(pytaniaLista.ExportToTxt()); });
            File.WriteAllLines(SciezkaDoPliku,pytaniaPoFormacie);
            

       
        }
    }
}
