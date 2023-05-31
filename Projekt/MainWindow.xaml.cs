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
using static System.Net.Mime.MediaTypeNames;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int l_odpowiedzi { get; set; }
        public List<Odpowiedz> listaOdpowiedzi { get; set; }
        public Pytanie pytanie = new Pytanie();
        public Collection<Pytanie> Pytania { get; } = new ObservableCollection<Pytanie>();
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            listaOdpowiedzi = new List<Odpowiedz>();
            pytanie = new Pytanie { Tresc ="test2",Odpowiedzi =listaOdpowiedzi};
            lista.ItemsSource = Pytania;
            trescPytaniaTextBox.DataContext = new Pytanie { Tresc = "test3", Odpowiedzi = listaOdpowiedzi }; ;
            //listaOdpowiedzi.Add(new Odpowiedz());
           // listaOdpowiedzi.Add(new Odpowiedz());
          //  odpowiedzbindtest.DataContext = listaOdpowiedzi[0];
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

        public void zmienOdpowiedzi() {

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

                    listaOdpowiedzi.Add(new Odpowiedz());

                    Binding bindingCheckbox = new Binding();
                    bindingCheckbox.Path = new PropertyPath("poprawnosc");
                 //   checkBox.SetBinding(CheckBox.ContentProperty, bindingCheckbox);
                    checkBox.DataContext = listaOdpowiedzi[i + childrenCount];


                    TextBox textBox = new TextBox();
                    textBox.Margin = new Thickness(5);
                    textBox.Width = 300;
                    textBox.TextWrapping = TextWrapping.Wrap;

                    Binding bindingTextBox = new Binding();
                    bindingTextBox.Path = new PropertyPath("tresc");

                    textBox.SetBinding(TextBox.TextProperty, bindingTextBox);
                    checkBox.DataContext = listaOdpowiedzi[i + childrenCount];


                    wrapPanel.Children.Add(checkBox);
                    wrapPanel.Children.Add(textBox);
                    wrapPanel.SetValue(Grid.RowProperty, i + childrenCount);
                    odpowiedziGrid.Children.Add(wrapPanel);

                }
            }
            else if (difference > 0)
                for (int i = 0; i < difference; i++)
                {
                    odpowiedziGrid.Children.RemoveAt(odpowiedziGrid.Children.Count - 1);
                    listaOdpowiedzi.RemoveAt(odpowiedziGrid.Children.Count);
                }

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

            zmienOdpowiedzi();

        }
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

        private void dodajPytanieClick(object sender, RoutedEventArgs e)
        {
            pytanie = new Pytanie("");
        }

        private void usunPytanieClick(object sender, RoutedEventArgs e)
        {
            Pytania.RemoveAt(lista.SelectedIndex);

        }
    }
}
