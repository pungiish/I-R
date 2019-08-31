namespace IDE.Model
{
    using System.Collections.Generic;

    public class Metode
    {
        private List<string> _metode1;
        private string _title;
        private bool _metoda;
        public Metode()
        {
            _metode1 = new List<string>();
        }
        public List<string> Metode1
        {
            get
            {
                return _metode1;
            }
            set
            {
                _metode1 = value;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public bool Metoda
        {
            get
            {
                return _metoda;
            }
            set
            {
                _metoda = value;
            }
        }
    }

}
