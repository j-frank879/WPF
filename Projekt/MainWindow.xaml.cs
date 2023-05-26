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
namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int l_testow { get; set; }
        public Collection<Pytanie> Pytania { get; } = new ObservableCollection<Pytanie>();
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //lista.ItemsSource = Pytania;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

        private void l_testowChanged(object sender, TextChangedEventArgs e)
        {
            if (l_testowTextBox.Text == "")
                l_testowTextBox.Text = "0";
            else l_testow= Int32.Parse(l_testowTextBox.Text);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestsWindow testsWindow = new TestsWindow();
            testsWindow.Show();
        }
    }
}
