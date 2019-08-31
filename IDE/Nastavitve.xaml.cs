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
            //InitializeComponent();
            //DataContext = this;
            //ogrodja.ItemsSource = Ogrodjas;
            //shrani.IsChecked = Properties.Settings.Default.shrani;
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
    }

}
