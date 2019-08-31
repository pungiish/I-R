using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IDE
{
    /// <summary>
    /// Interaction logic for Nastavitve.xaml
    /// </summary>
    public partial class Nastavitve : Window
    {
        public Settings settings = new Settings();

        public Nastavitve()
        {
            InitializeComponent();


            settings = Properties.Settings.Default.CustomSettings;
            DataContext = settings;
        }
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            settings.programskiJezik.Add(new ProgramskiJezik(txtBox.Text, new string[] { "tip5", "tip6" }));
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (programskiJeziki.SelectedItem != null)
                (programskiJeziki.SelectedItem as ProgramskiJezik).Name = txtBox.Text;
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (programskiJeziki.SelectedItem != null)
            {
                settings.programskiJezik.Remove(programskiJeziki.SelectedItem as ProgramskiJezik);
                tipi1.ItemsSource = null;
            }
        }
        private void btnAddOgrodje_Click(object sender, RoutedEventArgs e)
        {
            settings.ogrodjas.Add(new Ogrodje(txtBox1.Text));
        }

        private void btnChangeOgrodje_Click(object sender, RoutedEventArgs e)
        {
            if (ogrodja.SelectedItem != null)
                (ogrodja.SelectedItem as Ogrodje).Name = txtBox1.Text;
        }

        private void btnDeleteOgrodje_Click(object sender, RoutedEventArgs e)
        {
            if (ogrodja.SelectedItem != null)
                settings.ogrodjas.Remove(ogrodja.SelectedItem as Ogrodje);
        }
     
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.CustomSettings = settings;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.shrani = false;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.shrani = true;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

        }


        private void tipi_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                ProgramskiJezik programskiJezik = (ProgramskiJezik)item;
                this.tipi1.ItemsSource = programskiJezik.tip;
            }
        }
    }
    [Serializable]
    public class ProgramskiJezik
    {
        private string name;
        public string[] tip { get; set; }
        public List<string> tipi
        {
            get
            {
                return toList();
            }
        }

        public ProgramskiJezik(string name, string[] tip)
        {
            this.name = name;
            this.tip = tip;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                }
            }
        }

        public List<string> toList()
        {
            List<string> l = new List<string>();
            foreach (string item in tip)
            {
                l.Add(item);
            }
            return l;
        }

    }
    [Serializable]
    public class Ogrodje
    {
        private string name;

        public Ogrodje(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                }
            }
        }
    }

    [Serializable]
    public class Settings
    {
        public ObservableCollection<ProgramskiJezik> programskiJezik { get; set; } = new ObservableCollection<ProgramskiJezik>();
        public ObservableCollection<Ogrodje> ogrodjas { get; set; } = new ObservableCollection<Ogrodje>();
    }


    public class View
    {
        public Settings Settings { get; set; }
        public string tip;

        public string Tip
        {
            get { return Settings.programskiJezik.First().tip[0]; }
        }

        public View()
        {
            Settings = new Settings();
        }

    }
}
