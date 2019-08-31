namespace IDE.Model
{
    using System.Collections.ObjectModel;
    public class Hierarhija
    {
        private string _title;
        private ObservableCollection<Hierarhija> _items;
	    public Hierarhija()
	    {
            _items = new ObservableCollection<Hierarhija>();
    	}
        public ObservableCollection<Hierarhija> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
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
    }
}
