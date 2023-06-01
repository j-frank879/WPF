using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6
{
    public class Odpowiedz : INotifyPropertyChanged
    {
            public string tresc;
            public string Tresc { get { return tresc; } set { tresc = value;  } }

        public bool poprawnosc;
        public bool Poprawnosc { get { return poprawnosc; } set { poprawnosc = value; } }

            public Odpowiedz(string tresc,bool poprawnosc)
            {
                this.tresc = tresc;
            this.poprawnosc = poprawnosc;
            }

        public Odpowiedz()
        {
            this.tresc ="odpowiedz";
            this.poprawnosc = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string property)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this,
                    new PropertyChangedEventArgs(property));
            }
            
        
    }
}
