using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Hierarhija
{
    public class Hierarhija
    {
        public string Title
        { get; set; }
        public ObservableCollection<Hierarhija> Items
        { get; set;}    
	    public Hierarhija()
	    {
            this.Items = new ObservableCollection<Hierarhija>();
    	}
    }
}
