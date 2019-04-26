using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IDE
{
    /// <summary>
    /// Interaction logic for Nastavitve.xaml
    /// </summary>
    public partial class Nastavitve : Window
    {
        public ObservableCollection<string> ProgramskiJeziks { get; set; }
        public ObservableCollection<KeyValuePair<String,String>> TipiProjektov { get; set; }
        public ObservableCollection<string> Ogrodjas { get; set; }
        public Nastavitve()
        {
            Ogrodjas = new ObservableCollection<string>();
            ProgramskiJeziks = Properties.Settings.Default.programskiJezik;
            TipiProjektov = Properties.Settings.Default.tipiProjektov;
            Ogrodjas = Properties.Settings.Default.ogrodja;

           if (TipiProjektov.Count == 0)
                Inicializiraj_Tipe_Projektov();
           if(ProgramskiJeziks == null)
                ProgramskiJeziks = new ObservableCollection<string>();
            InitializeComponent();
            DataContext = this;
            programskiJezik.ItemsSource = ProgramskiJeziks;
            ogrodja.ItemsSource = Ogrodjas;

        }
        private void Inicializiraj_Tipe_Projektov()
        {
            TipiProjektov = new ObservableCollection<KeyValuePair<string, string>>();
            TipiProjektov.Add(new KeyValuePair<string, string>("c++", "Windows Console App"));
            TipiProjektov.Add(new KeyValuePair<string, string>("c++", "Static Library"));
            TipiProjektov.Add(new KeyValuePair<string, string>("c++", "Empty Project"));
            TipiProjektov.Add(new KeyValuePair<string, string>("c", "Empty Project"));
            TipiProjektov.Add(new KeyValuePair<string, string>("c", "Empty Solution"));
            TipiProjektov.Add(new KeyValuePair<string, string>("c", "Empty Test"));
            TipiProjektov.Add(new KeyValuePair<string, string>("c#", "WPF App"));
            TipiProjektov.Add(new KeyValuePair<string, string>("c#", "WinForms App"));
            TipiProjektov.Add(new KeyValuePair<string, string>("c#", "Empty Project"));
            Properties.Settings.Default.tipiProjektov = TipiProjektov;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.programskiJezik == null)
            {
                Properties.Settings.Default.programskiJezik = new ObservableCollection<string>();
            }
            Properties.Settings.Default.programskiJezik.Add(txtBox.Text);
            ProgramskiJeziks.Add(txtBox.Text);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Reset();
            Properties.Settings.Default.programskiJezik.Remove(programskiJezik.SelectedItem.ToString());
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            Properties.Settings.Default.ogrodja.Add(txtBox1.Text);
            Properties.Settings.Default.Save();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Reset();
            Properties.Settings.Default.ogrodja.Remove(ogrodja.SelectedItem.ToString());
        }
        //https://stackoverflow.com/questions/10207888/wpf-listview-detect-when-selected-item-is-clicked
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            programskiJezik.SelectedItems.Clear();
            ListViewItem item = sender as ListViewItem;
            if (item != null)
            {
                item.IsSelected = true;
                programskiJezik.SelectedItem = item;
                ListViewItem_PreviewMouseLeftButtonUp(sender, e);

            }

        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Properties.Settings.Default.Reload();
            TipiProjektov = Properties.Settings.Default.tipiProjektov;
            ListViewItem item = sender as ListViewItem;
            KeyValuePair<string, string>[] arr = new KeyValuePair<string, string>[TipiProjektov.Count];
            TipiProjektov.CopyTo(arr, 0);
            if (item != null && item.IsSelected)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if(programskiJezik.SelectedItem.ToString().ToLower() != arr[i].Key.ToLower())
                    {
                        TipiProjektov.Remove(arr[i]);
                    }
                }
            }
            tipAplikacij.ItemsSource = null;
            tipAplikacij.ItemsSource = TipiProjektov;
        }
    }
    
}
