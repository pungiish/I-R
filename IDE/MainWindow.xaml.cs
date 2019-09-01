using IDE.Model;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Serialization;
using WpfControlLibrary1;

namespace IDE
{
    /// <summary>

    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer DispatcherTimer { get; private set; }
        public int sec = Properties.Settings.Default.sec;
        public int min = Properties.Settings.Default.min;
        public int hr = Properties.Settings.Default.hr;
        TreeViewItem tviI = new TreeViewItem() { Header = "Hiter Projekt", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };
        TreeViewItem tviIA = new TreeViewItem() { Header = "Main.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };
        TreeViewItem tviIB = new TreeViewItem() { Header = "MainWindow.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };
        public Struktura s = new Struktura();
        Settings cs = new Settings();
        public MainWindow()
        {
            if (Properties.Settings.Default.shrani == true)
            {
                DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                DispatcherTimer.Tick += new EventHandler(DispatcherTimerTick);
                DispatcherTimer.Interval = new TimeSpan(hr, min, sec);
                DispatcherTimer.Start();
            }
            InitializeComponent();
            InitializeSettings();
            UserControl1 userControl = new UserControl1();
            userControl.OnMethodSelect += (senser, e) =>
            {
                PassThrough("MethodChange", senser, e);
            };
            userControl.OnFileSelect += (senser, e) =>
            {
                PassThrough("FileChange", senser, e);
            };

        }
        public void InitializeSettings()
        {
            cs = Properties.Settings.Default.CustomSettings;
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
            }
        }
        private void DispatcherTimerTick(object sender, EventArgs e)
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
            strukturaProjekta.Items.Clear();
            strukturaProjekta.Items.Add(tviI);
            strukturaProjekta.Items.Add(tviIA);
            strukturaProjekta.Items.Add(tviIB);
            s.ime = "Hiter Projekt";
            s.programskiJezik = "c#";
            s.ogrodje = ".NET Core";
            s.tip = "WPF app";
        }

        private void MenuItem_Click_ZapriProjekt(object sender, RoutedEventArgs e)
        {
            if (strukturaProjekta.Items.Count != 0)
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
                strukturaProjekta.Items.Clear();
            }



        }

        private void StrukturaProjekta_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            listView.Items.Clear();
            txtEditor.Document.Blocks.Clear();
            var item = (e.NewValue as TreeViewItem);
            if (item == null)
            {

            }
            else
            {


                strukturaProjekta.Tag = item;
                if (item.Items.Count == 0)
                {
                    string name = item.Header.ToString();
                    if (name.Contains("Main.cs"))
                    {
                        txtEditor.AppendText($" using System;\n namespace HelloWorld\n {{\n class Hello\n {{\n static void Main()\n{{\n Console.WriteLine('Hello World!');\n }}\n }}\n }}");
                        this.listView.Items.Add(new MyItem { Id = 4, Metoda = " static void Main()" });

                    }
                    else if (name.Contains("MainWindow.cs"))
                    {
                        txtEditor.AppendText($"using System.Windows;\n using System.Windows.Controls;\n namespace IDE\n {{\n public partial class MainWindow : Window\n{{\npublic MainWindow()\n{{\nInitializeComponent();\n}}");
                        this.listView.Items.Add(new MyItem { Id = 5, Metoda = "MainWindow()" });
                        this.listView.Items.Add(new MyItem { Id = 6, Metoda = "InitializeComponent()" });
                    }
                    else if (name.Contains("Settings.cs"))
                    {
                        txtEditor.AppendText($"namespace IDE.Properties {{ \ninternal sealed partial class Settings {{ \npublic Settings() {{ \n}}\n");
                        this.listView.Items.Add(new MyItem { Id = 7, Metoda = "Settings()" });
                    }
                    else if (name.Contains("Main.cpp"))
                    {
                        txtEditor.AppendText($"#include <iostream>\n using namespace std;\n int main()\n {{\n cout << 'Hello, World!';\n return 0;\n }});");
                        this.listView.Items.Add(new MyItem { Id = 3, Metoda = "int main()" });
                    }
                }
            }
        }

        private void MenuItem_Click_DodajDatoteko(object sender, RoutedEventArgs e)
        {
            strukturaProjekta.Items.Add(new TreeViewItem() { Header = "newItem.txt" });


        }

        private void MenuItem_Click_OdstraniDatoteko(object sender, RoutedEventArgs e)
        {
            if (strukturaProjekta.SelectedItem == null)
            {
                System.Windows.MessageBox.Show($"There is no selected item!");

            }
            else
                strukturaProjekta.Items.Remove(strukturaProjekta.SelectedItem);
        }

        private void ListView_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                txtEditor.Document.Blocks.Clear();
                MyItem myItem = (MyItem)item;
                if (myItem.Id == 1)
                {
                    txtEditor.AppendText($" listView.Items.Clear(); var item = (e.NewValue as TreeViewItem);strukturaProjekta.Tag = item if (item.Items.Count == 0");
                }
                if (myItem.Id == 2)
                {
                    txtEditor.AppendText($"var item = (sender as ListView).SelectedItem;" + "if (item != null)");
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };
            if (o.ShowDialog() == true)
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Struktura));
                TextReader reader = new StreamReader(o.FileName);
                s = (Struktura)deserializer.Deserialize(reader);
                reader.Close();
                strukturaProjekta.Items.Add(new TreeViewItem() { Header = s.ime, HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                if (s.programskiJezik != null && s.tip != null & s.ogrodje != null)
                {
                    if (s.programskiJezik.ToLower() == "c++")
                    {

                        if (s.tip.ToLower() == "console app")
                        {
                            strukturaProjekta.Items.Add(new TreeViewItem() { Header = "Main.cpp", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });

                        }

                    }
                    else if (s.programskiJezik.ToLower() == "c#")
                    {
                        if (s.tip.ToLower() == "wpf app")
                        {
                            strukturaProjekta.Items.Add(new TreeViewItem() { Header = "MainWindow.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                            strukturaProjekta.Items.Add(new TreeViewItem() { Header = "MainWindow.xaml", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                            strukturaProjekta.Items.Add(new TreeViewItem() { Header = "Settings.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });
                        }

                        else if (s.tip.ToLower() == "console app")
                        {
                            strukturaProjekta.Items.Add(new TreeViewItem() { Header = "Main.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch });

                        }
                    }
                }
                Console.WriteLine(s);
            }
        }

    }

    public class MyItem
    {
        public int Id { get; set; }

        public string Metoda { get; set; }
    }
}

