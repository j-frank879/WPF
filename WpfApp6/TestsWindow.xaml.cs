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
    }
}
