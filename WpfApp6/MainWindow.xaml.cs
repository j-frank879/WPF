using System;
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
        public MainWindow()
        {
            InitializeComponent();
            List<Pytanie> zaladujPytania = new ImportPytan().Import();
            zaladujPytania.ForEach(Pytania.Add);

            Pytania.Add(new Pytanie("Przykladowe pytanie 1"));
            Pytania.Add(new Pytanie("Przykladowe pytanie 2"));
            Pytania.Add(new Pytanie("Przykladowe pytanie 3"));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaPytan.ItemsSource = Pytania;

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
                //generacja testow
                Collection<Test> tests = generujTesty();
                TestsWindow testsWindow = new TestsWindow(tests);
                testsWindow.Show();
            }

           
        }

        private void dodajPytanieClick(object sender, RoutedEventArgs e)
        {
            Pytania.Add(new Pytanie("Przykładowe pytanie"));

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
            if (listaPytan.SelectedIndex < 0 || listaPytan.SelectedIndex < 0)
                return;
            if (Pytania[listaPytan.SelectedIndex].Odpowiedzi.Count > 2)

                Pytania[listaPytan.SelectedIndex].Odpowiedzi.RemoveAt(listaOdpowiedzi.SelectedIndex);
           // else bladTextBlock.Text = "Nie można usunąć odpowiedzi. Minimalna liczba odpowiedzi to 2.";

        }
        private Collection<Test> generujTesty()
        { Collection<Test> testy=new ObservableCollection<Test>();
            Random rnd=new Random();
            //petla testow
            for (int i = 0; i < wybranaLiczbaTestow; i++)
            {
                Collection<Pytanie> pytaniaTestu = new ObservableCollection<Pytanie>();
                Collection<Pytanie> tmp_pytania = new ObservableCollection<Pytanie>(Pytania);
                //petla pytan
                for (int j = 0;j<wybranaLiczbaPytan;j++)
                {
                    int randPytanie = rnd.Next(0, tmp_pytania.Count - 1);
                    Pytanie p = tmp_pytania[randPytanie];

                    Collection<Odpowiedz> odpowiedziPytania = new ObservableCollection<Odpowiedz>();
                    int p_count = p.Odpowiedzi.Count;
                    //petla odpowiedzi
                    for (int k = 0; k < p_count; k++)
                    {
                        int randOdpowiedz = rnd.Next(0, p.Odpowiedzi.Count-1);

                        odpowiedziPytania.Add(p.Odpowiedzi[randOdpowiedz]);
                        p.Odpowiedzi.RemoveAt(randOdpowiedz);

                    }
                    p.Odpowiedzi = odpowiedziPytania;

                    pytaniaTestu.Add(p);
                    tmp_pytania.RemoveAt(randPytanie);

                }
                testy.Add(new Test(pytaniaTestu));
            }
            
            return testy;
        }
    }
}
    
