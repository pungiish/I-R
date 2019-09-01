using System.ComponentModel;
using System.Xml.Serialization;

namespace IDE.Model
{
    class Struktura : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string Ime;
        private string ProgramskiJezik;
        private string Tip;
        private string Ogrodje;
        [XmlAttribute]
        public string ime { get { return Ime; } set { Ime = value; OnPropertyChanged(new PropertyChangedEventArgs("")); } }
        public string programskiJezik { get { return ProgramskiJezik; } set { ProgramskiJezik = value; OnPropertyChanged(new PropertyChangedEventArgs("")); } }
        public string tip { get { return Tip; } set { Tip = value; OnPropertyChanged(new PropertyChangedEventArgs("")); } }
        public string ogrodje { get { return Ogrodje; } set { Ogrodje = value; OnPropertyChanged(new PropertyChangedEventArgs("")); } }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}

