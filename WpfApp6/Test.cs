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
        public string Name { get; set; }
        public Collection<Pytanie> Pytania { get; set; } = new ObservableCollection<Pytanie>();
        public Test(int id)
        {
            Pytania = new ObservableCollection<Pytanie>();
            this.Name = "Test "+(id+1);
        }
        public Test(Collection<Pytanie> pytania, int id)
        {
            this.Pytania = pytania;
            this.Name = "Test " + (id + 1);
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
