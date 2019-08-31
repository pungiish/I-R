using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IDE
{
    /// <summary>
    /// Interaction logic for Nastavitve.xaml
    /// Malo zakomplicirano zaradi stalnega nastavljanja Observablov in njihovega spreminjanja.
    /// </summary>
    public partial class Nastavitve : Window
    {
        //public int _idProgramskiJezik { get; set; }
        //public ObservableCollection<KeyValuePair<int, string>> ProgramskiJeziks { get; set; }
        //public ObservableCollection<KeyValuePair<string, string>> TipiProjektov { get; set; }
        //public KeyValuePair<string, string>[] _tipiProjektov { get; set; }
        //public ObservableCollection<KeyValuePair<string, string>> projectList;
        //public ObservableCollection<string> Ogrodjas { get; set; }
        //public ListViewItem item { get; set; }
        public Settings settings = new Settings();

        public Nastavitve()
        {


            //Ogrodjas = new ObservableCollection<string>();
            //TipiProjektov = new ObservableCollection<KeyValuePair<string, string>>();
            //ProgramskiJeziks = Properties.Settings.Default.programskiJezik;
            //Ogrodjas = Properties.Settings.Default.ogrodja;
            //Inicializiraj_Tipe_Projektov();
            //if (ProgramskiJeziks.Count == 0)
            //{
            //    ProgramskiJeziks = new ObservableCollection<KeyValuePair<int, string>>();
            //    _idProgramskiJezik = 0;
            //}
            //else
            //{
            //    _idProgramskiJezik = ProgramskiJeziks.ElementAt(ProgramskiJeziks.Count - 1).Key;
            //    _idProgramskiJezik++;
            //}
            //_tipiProjektov = TipiProjektov.ToArray();
            //projectList = new ObservableCollection<KeyValuePair<string, string>>(_tipiProjektov);
            InitializeComponent();
            if (Properties.Settings.Default.CustomSettings.programskiJezik.Count == 0)
            {

                settings.programskiJezik.Add(new ProgramskiJezik("c++", new string[] { "tip1", "tip2" }));
                settings.programskiJezik.Add(new ProgramskiJezik("c", new string[] { "tip1" }));
                settings.ogrodjas.Add(new Ogrodje("WPF APP"));
            }
            else
                settings = Properties.Settings.Default.CustomSettings;
            DataContext = settings;
            //DataContext = this;
            //ogrodja.ItemsSource = Ogrodjas;
            //shrani.IsChecked = Properties.Settings.Default.shrani;
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
        private void Inicializiraj_Tipe_Projektov()
        {
            //TipiProjektov.Add(new KeyValuePair<string, string>("c++", "Windows Console App"));
            //TipiProjektov.Add(new KeyValuePair<string, string>("c++", "Static Library"));
            //TipiProjektov.Add(new KeyValuePair<string, string>("c++", "Empty Project"));
            //TipiProjektov.Add(new KeyValuePair<string, string>("c", "Empty Project"));
            //TipiProjektov.Add(new KeyValuePair<string, string>("c", "Empty Solution"));
            //TipiProjektov.Add(new KeyValuePair<string, string>("c", "Empty Test"));
            //TipiProjektov.Add(new KeyValuePair<string, string>("c#", "WPF App"));
            //TipiProjektov.Add(new KeyValuePair<string, string>("c#", "WinForms App"));
            //TipiProjektov.Add(new KeyValuePair<string, string>("c#", "Empty Project"));
            //Properties.Settings.Default.tipiProjektov = TipiProjektov;
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();
        }
        private void SaveProgramskeJezike(object sender, RoutedEventArgs e)
        {
            //if (Properties.Settings.Default.programskiJezik == null)
            //{
            //    //Properties.Settings.Default.programskiJezik = new ObservableCollection<KeyValuePair<int, string>>();
            //    Properties.Settings.Default.programskiJezik = ProgramskiJeziks;
            //}

            ////Properties.Settings.Default.programskiJezik.Add(txtBox.Text.ToUpper());
            //ProgramskiJeziks.Add(new KeyValuePair<int, string>(_idProgramskiJezik, txtBox.Text.ToUpper()));
            //Properties.Settings.Default.programskiJezik = ProgramskiJeziks;
            //_idProgramskiJezik++;
            //Properties.Settings.Default.tipiProjektov = projectList;
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if (item != null && item.IsSelected)
            //{
            //    ProgramskiJeziks.Remove((KeyValuePair<int, string>)programskiJezik.SelectedItem);
            //    Properties.Settings.Default.programskiJezik = ProgramskiJeziks;
            //    TipiProjektov = projectList;
            //    tipAplikacij.ItemsSource = TipiProjektov;
            //    Properties.Settings.Default.tipiProjektov = projectList;
            //    Properties.Settings.Default.Save();
            //    Properties.Settings.Default.Reload();
            //}

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Ogrodjas.Add(txtBox1.Text);
            //ogrodja.ItemsSource = Ogrodjas;
            //Properties.Settings.Default.ogrodja.Add(txtBox1.Text);
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Reset();
            //Properties.Settings.Default.ogrodja.Remove(ogrodja.SelectedItem.ToString());
            //Properties.Settings.Default.Reload();
        }
        //https://stackoverflow.com/questions/10207888/wpf-listview-detect-when-selected-item-is-clicked
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //programskiJezik.SelectedItems.Clear();
            //item = sender as ListViewItem;
            //if (item != null)
            //{
            //    item.IsSelected = true;
            //    programskiJezik.SelectedItem = item;
            //    ListViewItem_PreviewMouseLeftButtonUp(sender, e);

            //}
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Properties.Settings.Default.tipiProjektov = projectList;
            //Properties.Settings.Default.Reload();
            //KeyValuePair<int, string> l = new KeyValuePair<int, string>();
            //TipiProjektov = Properties.Settings.Default.tipiProjektov;
            //KeyValuePair<string, string>[] arr = new KeyValuePair<string, string>[TipiProjektov.Count];
            //TipiProjektov.CopyTo(arr, 0);
            //item = sender as ListViewItem;
            //if (item != null && item.IsSelected)
            //{
            //    for (int i = 0; i < arr.Length; i++)
            //    {
            //        l = (KeyValuePair<int, string>)programskiJezik.SelectedItem;

            //        if (l.Value.ToUpper() != arr[i].Key.ToUpper())
            //        {
            //            TipiProjektov.Remove(arr[i]);
            //        }
            //    }
            //    TipiProjektov = TipiProjektov;
            //    tipAplikacij.ItemsSource = TipiProjektov;
            //}
            ////Properties.Settings.Default.Save();
        }
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.CustomSettings = settings;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            //Properties.Settings.Default.tipiProjektov = projectList;

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.shrani = false;
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.shrani = true;
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

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
