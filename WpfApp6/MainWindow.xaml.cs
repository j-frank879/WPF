using Microsoft.Win32;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public Collection<Pytanie> Pytania { get; } = new ObservableCollection<Pytanie>();
        public int wybranaLiczbaPytan;
        public int wybranaLiczbaTestow;
        public string PlikImprtPytan;
        public string PlikImprtTest;
        public MainWindow()
        {
            InitializeComponent();
            new ImportPytan().Import(Pytania, @"..\..\..\ZestawPytan.txt");
            //Pytania.Add(new Pytanie("Przykladowe pytanie 1"));
            //Pytania.Add(new Pytanie("Przykladowe pytanie 2"));
            //Pytania.Add(new Pytanie("Przykladowe pytanie 3"));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaPytan.ItemsSource = Pytania;

        }

        private void WczytajPytania(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if(openFileDialog.ShowDialog() == true)
            {
                PlikImprtPytan = openFileDialog.FileName;
                new ImportPytan().Import(Pytania, PlikImprtPytan);
            }

        }
        private void ZapiszPytania(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                new ImportPytan().Export(Pytania, saveFileDialog.FileName);
            }
        }
        private void WczytajTesty(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ObservableCollection<Test> tests = new ObservableCollection<Test>();
            
                PlikImprtTest = openFileDialog.FileName;
                if (new ImportPytan().ImportTest(tests, PlikImprtTest) != null)
                {
                    TestsWindow testsWindow = new TestsWindow(tests);
                    testsWindow.Show();
                }
            }

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void generujTestyClick(object sender, RoutedEventArgs e)
        {GeneracjaTestowWindow generacjaTestowWindow = new GeneracjaTestowWindow(Pytania.Count);
            generacjaTestowWindow.ShowDialog();
            if (generacjaTestowWindow.DialogResult == true)
            {
                wybranaLiczbaPytan = Int32.Parse(generacjaTestowWindow.l_PytanTextBox.Text);
                wybranaLiczbaTestow = Int32.Parse(generacjaTestowWindow.l_TestowTextBox.Text);
                TestsWindow testsWindow = new TestsWindow(wybranaLiczbaTestow, wybranaLiczbaPytan, Pytania);
                testsWindow.Show();
            }

           
        }

        private void dodajPytanieClick(object sender, RoutedEventArgs e)
        {
            Pytania.Add(new Pytanie());

        }

        private void usunPytanieClick(object sender, RoutedEventArgs e)
        {
            if (listaPytan.SelectedIndex >= 0)
                Pytania.RemoveAt(listaPytan.SelectedIndex);

        }

        private void dodajOdpowiedzClick(object sender, RoutedEventArgs e)
        {
            if (listaPytan.SelectedIndex < 0)
                return;
            if (Pytania[listaPytan.SelectedIndex].Odpowiedzi.Count < 20)

                Pytania[listaPytan.SelectedIndex].Odpowiedzi.Add(new Odpowiedz());
           // else bladTextBlock.Text = "Nie można dodać więcej odpowiedzi. Maksymalna liczba odpowiedzi to 20.";
        }

        private void usunOdpowiedzClick(object sender, RoutedEventArgs e)
        {
            if (listaPytan.SelectedIndex < 0 || listaOdpowiedzi.SelectedIndex < 0)
                return;
            if (Pytania[listaPytan.SelectedIndex].Odpowiedzi.Count > 2)

                Pytania[listaPytan.SelectedIndex].Odpowiedzi.RemoveAt(listaOdpowiedzi.SelectedIndex);
           // else bladTextBlock.Text = "Nie można usunąć odpowiedzi. Minimalna liczba odpowiedzi to 2.";

        }
        
    }
}
    
