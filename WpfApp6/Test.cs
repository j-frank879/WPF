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
        public int id;
        public int Id { get { return id; } set { id = value; } }
        public Collection<Pytanie> Pytania { get; set; } = new ObservableCollection<Pytanie>();
        public Test(int id)
        {
            Pytania = new ObservableCollection<Pytanie>();
            this.id = id+1;
        }
        public Test(Collection<Pytanie> pytania, int id)
        {
            this.Pytania = pytania;
            this.id = id + 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                new PropertyChangedEventArgs(property));
        }

        public string Display
        {
            get
            {
                return "Test "+id.ToString();
            }

        }
    }
}
