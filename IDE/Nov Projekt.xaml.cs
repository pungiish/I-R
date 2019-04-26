using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IDE
{
    /// <summary>
    /// Interaction logic for Nov_Projekt.xaml
    /// </summary>
    public partial class Nov_Projekt : Window
    {
        public ObservableCollection<string> ProgramskiJeziks { get; set; }
        public ObservableCollection<KeyValuePair<String, String>> TipiProjektov { get; set; }
        public ObservableCollection<string> Ogrodjas { get; set; }
        public Nov_Projekt()
        {
            ProgramskiJeziks = Properties.Settings.Default.programskiJezik;
            Ogrodjas = Properties.Settings.Default.ogrodja;
            TipiProjektov = Properties.Settings.Default.tipiProjektov;
            if (TipiProjektov == null)
                TipiProjektov = new ObservableCollection<KeyValuePair<string, string>>();
            if (Ogrodjas == null)
               Ogrodjas = new ObservableCollection<string>();
            if (ProgramskiJeziks == null)
                ProgramskiJeziks = new ObservableCollection<string>();

            InitializeComponent();
            DataContext = this;

            //jeziki.ItemsSource = ProgramskiJeziks;
            //tipi.ItemsSource = TipiProjektov;
            //ogrodja.ItemsSource = Ogrodjas;

        }

        private void jeziki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Reload();
            TipiProjektov = Properties.Settings.Default.tipiProjektov;
            KeyValuePair<string, string>[] arr = new KeyValuePair<string, string>[TipiProjektov.Count];
            TipiProjektov.CopyTo(arr, 0);
            foreach (var items in arr)
            {
                if (items.Key == null)
                {
                    continue;
                }
                if (items.Key.ToLower() != jeziki.SelectedItem.ToString().ToLower())
                {
                    TipiProjektov.Remove(items);
                }
            }
            tipi.ItemsSource = TipiProjektov;
        }

        private void Shrani(object sender, RoutedEventArgs e)
        {
            string[] files;
            string path = Directory.GetCurrentDirectory();
            files = Directory.GetFiles(path);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = path;
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            DialogResult fbd = saveFileDialog.ShowDialog();
            if (fbd == System.Windows.Forms.DialogResult.OK)
            {
                //File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
            }
        }
    }
}
