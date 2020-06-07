using IDE.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Xml.Serialization;
using WpfControlLibrary1;

namespace IDE
{
    /// <summary>
    
    /// </summary>
    public partial class MainWindow : Window
    {
        Storyboard lblStoryboard = new Storyboard();
        public DispatcherTimer dispatcherTimer { get; private set; }
        public int sec = Properties.Settings.Default.sec;
        public int min = Properties.Settings.Default.min;
        public int hr = Properties.Settings.Default.hr;
        public UserControl1 uc;
        public Struktura s = new Struktura();
        public TreeView struktura;
        public List<TreeViewItem> strukturaProjekta = new List<TreeViewItem>();
        public event EventHandler ustvariProjekt;

        public MainWindow()
        {
            if (Properties.Settings.Default.shrani == true)
            {
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(hr, min, sec);
                dispatcherTimer.Start();
            }
            InitializeComponent();
            
            UserControl1 uc = usr;
            var text = uc.FindName("txtEditor") as RichTextBox;
            struktura = uc.FindName("strukturaProjekta") as TreeView;
            uc.OnMethodSelect += (senser, e) =>
            {
                PassThrough("MethodChange", senser, e);
            };
            uc.OnFileSelect += (senser, e) =>
            {
                PassThrough("FileChange", senser, e);
                

            };

        }

        
        public void PassThrough(string action, object senser, EventArgs e)
        {
            if (action == "MethodChange")
            {
                Console.WriteLine(action + "\n" + senser + "\n" + e);
            }
            else if (action == "FileChange")
            {
                Console.WriteLine(action + "\n" + senser + "\n" + e);
                uc = (UserControl1)senser;
                uc.UserControlStrukturaProjekta.Items.Clear();
                uc.richText.Document.Blocks.Clear();
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void Izhod_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_OdpriNastavitve(object sender, RoutedEventArgs e)
        {
            Nastavitve subWindow = new Nastavitve();
            subWindow.ShowDialog();
        }

        private void MenuItem_Click_NovProjekt(object sender, RoutedEventArgs e)
        {
            Nov_Projekt nov = new Nov_Projekt();
            nov.ShowDialog();
        }

        private void MenuItem_Click_UstvariProjekt(object sender, RoutedEventArgs e)
        {

            TreeViewItem tviI = new TreeViewItem() { Header = "Hiter Projekt", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };
            TreeViewItem tviIA = new TreeViewItem() { Header = "Main.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };
            TreeViewItem tviIB = new TreeViewItem() { Header = "Main.cpp", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };
            strukturaProjekta.Add(tviI);
            strukturaProjekta.Add(tviIA);
            strukturaProjekta.Add(tviIB);
            ustvariProjekt?.Invoke(this, e);

            if (struktura != null)
            {
                struktura.Items.Add(tviI);
                struktura.Items.Add(tviIA);
                struktura.Items.Add(tviIB);

            }


        }

        private void MenuItem_Click_ZapriProjekt(object sender, RoutedEventArgs e)
        {
            if (struktura.Items.Count != 0)
            {

                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "XML Files (*.xml)|*.xml",
                    FileName = s.ime
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Struktura));
                    TextWriter writer = new StreamWriter(saveFileDialog.FileName);
                    serializer.Serialize(writer, s);
                    writer.Close();
                }
                struktura.Items.Clear();
            }



        }
        private void MenuItem_Click_DodajDatoteko(object sender, RoutedEventArgs e)
        {
           struktura.Items.Add(new TreeViewItem() { Header = "Main.cs" });


        }

        private void MenuItem_Click_OdstraniDatoteko(object sender, RoutedEventArgs e)
        {
            if (struktura.SelectedItem == null)
            {
                System.Windows.MessageBox.Show($"There is no selected item!");

            }
            else
                struktura.Items.Remove(struktura.SelectedItem);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog o = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };
            if (o.ShowDialog() == true)
            {
                struktura.Items.Clear();
                XmlSerializer deserializer = new XmlSerializer(typeof(Struktura));
                TextReader reader = new StreamReader(o.FileName);
                s = (Struktura)deserializer.Deserialize(reader);
                reader.Close();
                struktura.Items.Add(new TreeViewItem() { Header = s.ime, HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                if (s.programskiJezik != null && s.tip != null & s.ogrodje != null)
                {
                    if (s.programskiJezik.ToLower() == "c++")
                    {

                        if (s.tip.ToLower() == "console app")
                        {
                            struktura.Items.Add(new TreeViewItem() { Header = "Main.cpp", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });

                        }

                    }
                    else if (s.programskiJezik.ToLower() == "c#")
                    {
                        if (s.tip.ToLower() == "wpf app")
                        {
                            struktura.Items.Add(new TreeViewItem() { Header = "MainWindow.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                            struktura.Items.Add(new TreeViewItem() { Header = "MainWindow.xaml", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                            struktura.Items.Add(new TreeViewItem() { Header = "Settings.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                        }

                        else if (s.tip.ToLower() == "console app")
                        {
                            struktura.Items.Add(new TreeViewItem() { Header = "Main.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });

                        }
                    }
                }
        }

        }

        //private void listView_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var item = (sender as ListView).SelectedItem;
        //    if (item != null)
        //    {
        //        txtEditor.Document.Blocks.Clear();
        //        MyItem myItem = (MyItem)item;
        //        if (myItem.Id == 1)
        //        {
        //            txtEditor.AppendText($" listView.Items.Clear(); var item = (e.NewValue as TreeViewItem);strukturaProjekta.Tag = item if (item.Items.Count == 0");
        //        }
        //        if (myItem.Id == 2)
        //        {
        //            txtEditor.AppendText($"var item = (sender as ListView).SelectedItem;" + "if (item != null)");
        //        }
        //    }
        //}


    }

}

public class MyItem
{
    public int Id { get; set; }

    public string Metoda { get; set; }
}

