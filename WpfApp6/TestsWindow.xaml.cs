using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Collection<Collection<Pytanie>> testy;
        public TestsWindow(Collection<Pytanie> pytania, int wybranaLiczbaPytan, int wybranaLiczbaTestow)
        {
            Random rnd = new Random();
            List<int> indeksy;
            testy = new Collection<Collection<Pytanie>>();
            for(int i = 0;  i < wybranaLiczbaTestow; i++)
            {
                indeksy = Enumerable.Range(0, pytania.Count).ToList();
                testy.Add(new Collection<Pytanie>());
                for(int j = 0; j < wybranaLiczbaPytan; j++)
                {
                    int indeks = rnd.Next(0, pytania.Count-j);
                    testy[i].Add(pytania[indeksy[indeks]]);
                    indeksy.RemoveAt(indeks);
                }
            }
            InitializeComponent();
        }
    }
}
