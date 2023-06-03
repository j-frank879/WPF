using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6
{
    public class Test : INotifyPropertyChanged
    {

        public Collection<Pytanie> Pytania { get; set; } = new ObservableCollection<Pytanie>();
        public Test()
        {
            Pytania = new ObservableCollection<Pytanie>();
        }
        public Test(Collection<Pytanie> pytania)
        {
            this.Pytania = pytania;
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
