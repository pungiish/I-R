using IDE.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace IDE
{
    /// <summary>
    /// Interaction logic for Nov_Projekt.xaml
    /// </summary>
    public partial class Nov_Projekt : Window
    {
        public Settings settings;
        public Nov_Projekt()
        {
            settings = Properties.Settings.Default.CustomSettings;
            InitializeComponent();
            DataContext = settings;
        }

        private void jeziki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as System.Windows.Controls.ComboBox).SelectedItem;
            if (item != null)
            {
                ProgramskiJezik programskiJezik = (ProgramskiJezik)item;
                this.tipi.ItemsSource = programskiJezik.tip;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {

            Struktura s = new Struktura();
            string[] files;
            string path = Directory.GetCurrentDirectory();
            files = Directory.GetFiles(path);
            string fileName = txtBox.Text;
            var jezik = (ProgramskiJezik)programskiJezik.SelectedValue;
            string tip = (string)tipi.SelectedValue;
            var ogrodje = (Ogrodje)ogrodja.SelectedValue;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = path,
                Filter = "XML Files (*.XML)|*.XML",
                FileName = fileName
            };
            DialogResult fbd = saveFileDialog.ShowDialog();
            if (fbd == System.Windows.Forms.DialogResult.OK)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).s = s;
                s.ime = fileName;
                s.programskiJezik = jezik.Name.ToLower();
                s.tip = tip.ToLower();
                s.ogrodje = ogrodje.Name.ToLower();
                XmlSerializer serializer = new XmlSerializer(typeof(Struktura));
                TextWriter writer = new StreamWriter(saveFileDialog.FileName);
                serializer.Serialize(writer, s);
                writer.Close();
                ((MainWindow)System.Windows.Application.Current.MainWindow).struktura.Items.Clear();
                //File.WriteAllText(saveFileDialog.FileName, "Datoteka");
                ((MainWindow)System.Windows.Application.Current.MainWindow).struktura.Items.Add(new TreeViewItem() { Header = fileName, HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                if (jezik != null && tip != null & ogrodje != null)
                {
                    if (jezik.Name.ToLower() == "c++")
                    {

                        if (tip.ToLower() == "console app")
                        {
                            ((MainWindow)System.Windows.Application.Current.MainWindow).struktura.Items.Add(new TreeViewItem() { Header = "Main.cpp", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });

                        }

                    }
                    else if (jezik.Name.ToLower() == "c#")
                    {
                        if (tip.ToLower() == "wpf app")
                        {
                            ((MainWindow)System.Windows.Application.Current.MainWindow).struktura.Items.Add(new TreeViewItem() { Header = "MainWindow.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                            ((MainWindow)System.Windows.Application.Current.MainWindow).struktura.Items.Add(new TreeViewItem() { Header = "MainWindow.xaml", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                            ((MainWindow)System.Windows.Application.Current.MainWindow).struktura.Items.Add(new TreeViewItem() { Header = "Settings.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                        }

                        else if (tip.ToLower() == "console app")
                        {
                            ((MainWindow)System.Windows.Application.Current.MainWindow).struktura.Items.Add(new TreeViewItem() { Header = "Main.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });

                        }
                    }
                    this.Close();
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
