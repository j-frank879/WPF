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
        Collection<Pytanie> pytania = new Collection<Pytanie>();

        public Collection<Pytanie> Import()
        {
            var lines = File.ReadLines(@"..\..\..\ZestawPytan.txt");
            Collection<Pytanie> pytania = new Collection<Pytanie>();

            foreach (var line in lines)
            {
                Collection<Odpowiedz> odpowiedzi = new Collection<Odpowiedz>();
                string[] element = line.Split(";;");

                for (int i = 1; i < element.Length; i = i + 2)
                {
                    Odpowiedz odpowiedz = new Odpowiedz(element[i], Convert.ToBoolean(element[i+1]));
                    odpowiedzi.Add(odpowiedz);
                    Console.WriteLine(odpowiedz.ToString());
                }

                Pytanie nowePytanie = new Pytanie(element[0], odpowiedzi);
                pytania.Add(nowePytanie);

            }

            return pytania;
        }
    }
}
