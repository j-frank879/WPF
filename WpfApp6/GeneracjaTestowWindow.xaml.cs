using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Logika interakcji dla klasy GeneracjaTestowWindow.xaml
    /// </summary>
    public partial class GeneracjaTestowWindow : Window
    {
        public int liczbaPytanCount { get; set; }
        public GeneracjaTestowWindow(int liczbaPytanCount)
        {
            InitializeComponent();
            this.liczbaPytanCount = liczbaPytanCount;
        }

        private void okClick(object sender, RoutedEventArgs e)
        {if (l_TestowTextBox.Text.Length == 0 || l_TestowTextBox.Text == "")
            {MessageBox.Show("Liczba testów nie może wynosić 0","Niepoprawna liczba testów",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (l_PytanTextBox.Text.Length == 0 || l_PytanTextBox.Text == "")
            {
                MessageBox.Show("Liczba pytań nie może wynosić 0", "Niepoprawna liczba pytań", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Int32.Parse(l_PytanTextBox.Text) > liczbaPytanCount)
            {
                MessageBox.Show("Liczba pytań nie może być większa niż "+liczbaPytanCount +"(liczba pytań w bazie)", "Niepoprawna liczba pytań", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            Close();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
