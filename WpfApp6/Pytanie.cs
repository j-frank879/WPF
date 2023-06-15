using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6
{
    public class Pytanie : INotifyPropertyChanged
    {
        public string tresc;
        public string Tresc { get { return tresc; } set { tresc = value;  } }

        public Collection<Odpowiedz> Odpowiedzi { get; set; }= new ObservableCollection<Odpowiedz>();
        public Pytanie(string tresc)
        {
            this.tresc = tresc;
            this.Odpowiedzi.Add(new Odpowiedz("Niepoprawna odpowiedz", false));
            this.Odpowiedzi.Add(new Odpowiedz("Poprawna odpowiedz", true));
            this.Odpowiedzi.Add(new Odpowiedz("Niepoprawna odpowiedz", false));
        }
        public Pytanie()
        {

            this.Odpowiedzi.Add(new Odpowiedz());
            this.Odpowiedzi.Add(new Odpowiedz());
        }
        public Pytanie(string tresc, Collection<Odpowiedz> odpowiedzi)
        {
            this.tresc = tresc;
            this.Odpowiedzi = odpowiedzi;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                new PropertyChangedEventArgs(property));
        }
        public override string ToString()
        {
            return tresc;
        }
        public string Display
        {
            get
            {
                return tresc;
            }

        }
    }
}
