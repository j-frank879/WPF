using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp6
{
    public class ImportPytan
    {
        public Collection<Pytanie> Import(Collection<Pytanie> pytania, string SciezkaDoPliku)
        {
            var lines = File.ReadLines(SciezkaDoPliku);
            if (lines.First() == "To jest nagłówek pliku z pytaniami do projektu z WPF, jeżeli plik nie ma tej lini to jest nie kompatybilny z programem")
            {

                foreach (var line in lines)
                {
                    if (line != "To jest nagłówek pliku z pytaniami do projektu z WPF, jeżeli plik nie ma tej lini to jest nie kompatybilny z programem")
                    {
                        string[] element = line.Split(";;");

                        Pytanie nowePytanie = new Pytanie(element[0]);

                        for (int i = 1; i < element.Length; i = i + 2)
                        {
                            Odpowiedz odpowiedz = new Odpowiedz(element[i], Convert.ToBoolean(element[i + 1]));
                            nowePytanie.Odpowiedzi.Add(odpowiedz);
                        }

                        //Pytanie nowePytanie = new Pytanie(element[0], odpowiedzi);
                        pytania.Add(nowePytanie);
                    }
                }

                return pytania;
            }
            else
            {
                MessageBox.Show("Plik ma zły format lub nie został wygenerowany przez ten program", "Zły format pliku", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public void Export(Collection<Pytanie> pytania, string SciezkaDoPliku)
        {
            List<Pytanie> pytaniaLista = pytania.ToList();
            List<string> pytaniaPoFormacie = new List<string>();
            pytaniaLista.ForEach(pytaniaLista => { pytaniaPoFormacie.Add(pytaniaLista.ExportToTxt()); });
            File.WriteAllLines(SciezkaDoPliku,pytaniaPoFormacie);
        }

        public Collection<Test> ImportTest(Collection<Test> testy, string SciezkaDoPliku)
        {
            var lines = File.ReadLines(SciezkaDoPliku);
            //var lines = File.ReadLines(@"..\..\..\ZestawTestow.txt");
            Collection<Pytanie> pytania = new Collection<Pytanie>();
            int idTestu = 0;
            if (lines.First() == "To jest nagłówek pliku z testami do projektu z WPF, jeżeli plik nie ma tej lini to jest nie kompatybilny z programem")
            {

                foreach (var line in lines)
                {
                    if (line != "To jest nagłówek pliku z testami do projektu z WPF, jeżeli plik nie ma tej lini to jest nie kompatybilny z programem")
                    {
                        if (line == "To jest separator testu")
                        {
                            Test nowyTest = new Test(pytania, idTestu);
                            testy.Add(nowyTest);
                            pytania = new Collection<Pytanie>();
                            idTestu++;
                        }
                        else
                        {
                            string[] element = line.Split(";;");
                            Pytanie nowePytanie = new Pytanie(element[0]);
                            for (int i = 1; i < element.Length; i = i + 2)
                            {
                                Odpowiedz odpowiedz = new Odpowiedz(element[i], Convert.ToBoolean(element[i + 1]));
                                nowePytanie.Odpowiedzi.Add(odpowiedz);
                            }
                            pytania.Add(nowePytanie);
                        }
                    }
                }
                return testy;
            }
            else
            {
                MessageBox.Show("Plik ma zły format lub nie został wygenerowany przez ten program", "Zły format pliku", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public void ExportTest(Collection<Test> testy, string SciezkaDoPliku)
        {
            List<string> pytaniaPoFormacie = new List<string>();
            List<Test> testyLista = testy.ToList();

            testyLista.ForEach(test => {
                List<Pytanie> pytaniaLista = test.Pytania.ToList();
                pytaniaLista.ForEach(pytaniaLista => { pytaniaPoFormacie.Add(pytaniaLista.ExportToTxt()); });
                pytaniaPoFormacie.Add("To jest separator testu");
            });
            File.WriteAllLines(SciezkaDoPliku, pytaniaPoFormacie);
        }
    }
}
