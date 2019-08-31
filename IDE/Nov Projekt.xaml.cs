using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace IDE
{
    /// <summary>
    /// Interaction logic for Nov_Projekt.xaml
    /// </summary>
    public partial class Nov_Projekt : Window
    {
        public ObservableCollection<KeyValuePair<int, string>> ProgramskiJeziks { get; set; }
        public ObservableCollection<KeyValuePair<string, string>> TipiProjektov { get; set; }
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
                ProgramskiJeziks = new ObservableCollection<KeyValuePair<int, string>>();

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
            string key = ((KeyValuePair<int, string>)jeziki.SelectedItem).Value;
            foreach (var items in arr)
            {
                if (items.Key == null)
                {
                    continue;
                }
                if (items.Key != key.ToString().ToLower())
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
            saveFileDialog.Filter = "TXT Files (*.txt)|*.txt";
            DialogResult fbd = saveFileDialog.ShowDialog();
            if (fbd == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, "Datoteka");
            }
        }
    }
}
