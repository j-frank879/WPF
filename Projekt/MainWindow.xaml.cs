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
        public int l_odpowiedzi { get; set; }
        public  Pytanie pytanie { get; set; }
        public Collection<Pytanie> Pytania { get; } = new ObservableCollection<Pytanie>();
        public MainWindow()
        {
            InitializeComponent();
            pytanieGrid.DataContext = pytanie;
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lista.ItemsSource = Pytania;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

        
        private void l_odpowiedziChanged(object sender, TextChangedEventArgs e)
        {
            if (odpowiedziGrid == null || l_odpowiedziTextBox== null)
                    return;

            if (l_odpowiedziTextBox.Text == "")
                return;
            else l_odpowiedzi = Int32.Parse(l_odpowiedziTextBox.Text);

            if (l_odpowiedzi > 20) l_odpowiedziTextBox.Text = "20";
            else if (l_odpowiedzi < 2) l_odpowiedziTextBox.Text = "2";

            int childrenCount = odpowiedziGrid.Children.Count;
            int difference = childrenCount - l_odpowiedzi;


            if (difference < 0)
            {
                difference *= -1;
                for (int i = 0; i < difference; i++)
                {
                    WrapPanel wrapPanel = new WrapPanel();
                    CheckBox checkBox = new CheckBox();
                    checkBox.Margin = new Thickness(10);

                    TextBox textBox = new TextBox();
                    textBox.Margin = new Thickness(5);
                    textBox.Width = 300;
                    textBox.TextWrapping = TextWrapping.Wrap;

                    wrapPanel.Children.Add(checkBox);
                    wrapPanel.Children.Add(textBox);
                    wrapPanel.SetValue(Grid.RowProperty,i+childrenCount);
                    odpowiedziGrid.Children.Add(wrapPanel);
                    
                }
            }
            else if (difference > 0)
                for (int i = 0; i < difference; i++)
                {
                    odpowiedziGrid.Children.RemoveAt(odpowiedziGrid.Children.Count-1);
                }



        }
        /*<WrapPanel Grid.Row="0">
                                     </WrapPanel>
                                <WrapPanel Grid.Row="1">
                                    <CheckBox Margin="10"/>
                                    <TextBox Margin="5" Width="300"  Text="odpowiedz 2" TextWrapping="Wrap"/>
                                </WrapPanel>*/
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestsWindow testsWindow = new TestsWindow();
            testsWindow.Show();
        }

        private void l_odpowiedziTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (odpowiedziGrid == null || l_odpowiedziTextBox == null)
                return;
            if (l_odpowiedziTextBox.Text == "")
                l_odpowiedziTextBox.Text = "2";

        }
    }
}
