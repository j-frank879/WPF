using Microsoft.Win32;
using Syncfusion.DocIO.DLS;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Test> testy { get; } = new ObservableCollection<Test>();
        public TestsWindow(Collection<Test> testy)
        {
            this.testy = (ObservableCollection<Test>?)testy;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            testsList.ItemsSource = testy;
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
                    paragraph.AppendText("Test " + test.Id);
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

                    document.Save(savePath+"\\Test-" + test.id + ".docx");
                }
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
                    paragraph.AppendText("Test " + test.Id);
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

                    document.Save(savePath+"\\Test-" + test.id + "-odp.docx");
                }

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
