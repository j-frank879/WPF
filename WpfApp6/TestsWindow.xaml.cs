using Microsoft.Win32;
using Syncfusion.DocIO.DLS;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Logika interakcji dla klasy TestsWindow.xaml
    /// </summary>
    public partial class TestsWindow : Window
    {
        public ObservableCollection<Test> testy { get; set; } = new ObservableCollection<Test>();
        public TestsWindow(int wybranaLiczbaTestow, int wybranaLiczbaPytan, Collection<Pytanie> Pytania)
        {
            InitializeComponent();
            generujTesty(wybranaLiczbaTestow, wybranaLiczbaPytan, Pytania);
        }
        public TestsWindow(ObservableCollection<Test> testy)
        {
            InitializeComponent();
            this.testy = testy;
        }
        private void updateIndeksy()
        {
            for(int i = 1; i <= testy.Count; i++) { testy[i-1].Name = "Test " + (i);  }
        }

        private void generujTestyDocx(object sender, RoutedEventArgs e)
        {
            string dummyFileName = "Wybierz folder";
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = dummyFileName;

            if (sf.ShowDialog() == true)
            {
                string savePath = System.IO.Path.GetDirectoryName(sf.FileName);
                foreach (var test in testy)
                {
                    WordDocument document = new WordDocument();
                    //Adding a new section to the document.
                    WSection section = document.AddSection() as WSection;
                    IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();
                    paragraph.AppendText(test.Name);
                    paragraph = section.AddParagraph();
                    foreach (var pytanie in test.Pytania)
                    {
                        paragraph.AppendText(pytanie.tresc);
                        paragraph = section.AddParagraph();
                        foreach (var odpowiedz in pytanie.Odpowiedzi)
                        {
                            paragraph.AppendText("○ " + odpowiedz.tresc);
                            paragraph = section.AddParagraph();

                        }
                    }

                    document.Save(savePath+"\\Test-" + testy.IndexOf(test) + ".docx");
                }
            }
            MessageBox.Show("Wygenerowano testy");

        }

        private void generujTesty(int wybranaLiczbaTestow, int wybranaLiczbaPytan, Collection<Pytanie> Pytania)
        {
            Random rnd = new Random();
            //petla testow
            for (int i = 0; i < wybranaLiczbaTestow; i++)
            {
                Collection<Pytanie> pytaniaTestu = new ObservableCollection<Pytanie>();
                Collection<Pytanie> tmp_pytania = new ObservableCollection<Pytanie>(Pytania);
                //petla pytan
                for (int j = 0; j < wybranaLiczbaPytan; j++)
                {
                    int randPytanie = rnd.Next(0, tmp_pytania.Count - 1);
                    Pytanie p = tmp_pytania[randPytanie];

                    Collection<Odpowiedz> odpowiedziPytania = new ObservableCollection<Odpowiedz>();
                    int p_count = p.Odpowiedzi.Count;
                    //petla odpowiedzi
                    for (int k = 0; k < p_count; k++)
                    {
                        int randOdpowiedz = rnd.Next(0, p.Odpowiedzi.Count - 1);

                        odpowiedziPytania.Add(p.Odpowiedzi[randOdpowiedz]);
                        p.Odpowiedzi.RemoveAt(randOdpowiedz);

                    }
                    p.Odpowiedzi = odpowiedziPytania;

                    pytaniaTestu.Add(p);
                    tmp_pytania.RemoveAt(randPytanie);

                }
                testy.Add(new Test(pytaniaTestu, i));
            }
        }

        private void generujKluczDocx(object sender, RoutedEventArgs e)
        {
            string dummyFileName = "Wybierz folder";
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = dummyFileName;

            if (sf.ShowDialog() == true)
            {
                string savePath = System.IO.Path.GetDirectoryName(sf.FileName);
                foreach (var test in testy)
                {
                    WordDocument document = new WordDocument();
                    //Adding a new section to the document.
                    WSection section = document.AddSection() as WSection;
                    IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();
                    paragraph.AppendText(test.Name);
                    paragraph = section.AddParagraph();
                    foreach (var pytanie in test.Pytania)
                    {
                        paragraph.AppendText(pytanie.tresc);
                        paragraph = section.AddParagraph();
                        foreach (var odpowiedz in pytanie.Odpowiedzi)
                        {
                            if (odpowiedz.poprawnosc)
                                paragraph.AppendText("✔ " + odpowiedz.tresc);
                            else paragraph.AppendText("☓" + odpowiedz.tresc);
                            paragraph = section.AddParagraph();

                        }
                    }

                    document.Save(savePath+"\\Test-" + testy.IndexOf(test) + "-odp.docx");
                }

            }
            MessageBox.Show("Wygenerowano klucz odpowiedzi do testów.");

        }

        private void UsunTest_Click(object sender, RoutedEventArgs e)
        {
            if (listaTestow.SelectedIndex >= 0)
            {
                testy.RemoveAt(listaTestow.SelectedIndex);
                updateIndeksy();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaTestow.ItemsSource = testy;
        }

        private void ZaznaczOdpowiedzi_Click(object sender, RoutedEventArgs e)
        {
            if (listaTestow.SelectedIndex >= 0)
            {
                foreach (var pytanie in testy[listaTestow.SelectedIndex].Pytania)
                {
                    foreach (var odpowiedz in pytanie.Odpowiedzi)
                    {
                        if(odpowiedz.Poprawnosc)
                        odpowiedz.Zaznaczona = true;
                    }
                }
                var temp = listaTestow.SelectedIndex;
                listaTestow.SelectedIndex = -1;
                listaTestow.SelectedIndex = temp;
            }
        }

        private void UkryjOdpowiedzi_Click(object sender, RoutedEventArgs e)
        {
            if (listaTestow.SelectedIndex >= 0)
            { 
                foreach(var pytanie in testy[listaTestow.SelectedIndex].Pytania)
                {
                    foreach(var odpowiedz in  pytanie.Odpowiedzi)
                    {
                        odpowiedz.Zaznaczona = false;
                    }
                }
                var temp = listaTestow.SelectedIndex;
                listaTestow.SelectedIndex = -1;
                listaTestow.SelectedIndex = temp;
            }
        }
        private void ZapiszTesty(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                new ImportPytan().ExportTest(testy, saveFileDialog.FileName);
            }
        }
    }
}
