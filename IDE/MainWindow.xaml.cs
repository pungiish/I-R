using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfControlLibrary1;

namespace IDE
{
    /// <summary>
    
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer dispatcherTimer { get; private set; }
        public int sec = Properties.Settings.Default.sec;
        public int min = Properties.Settings.Default.min;
        public int hr = Properties.Settings.Default.hr;
        public string test = "RANDOMTEST";
        public UserControl1 uc;
        public TreeView struktura;

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
            if(struktura != null)
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

                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    // do smth
                }
                struktura.Items.Clear();
            }



        }

        //private void strukturaProjekta_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    listView.Items.Clear();
        //    txtEditor.Document.Blocks.Clear();
        //    var item = (e.NewValue as TreeViewItem);
        //    if (item == null)
        //    {

        //    }
        //    else
        //    {


        //        strukturaProjekta.Tag = item;
        //        if (item.Items.Count == 0)
        //        {
        //            string name = item.Header.ToString();
        //            if (name.Contains("Main.cs"))
        //            {
        //                txtEditor.AppendText($" using System;\n namespace HelloWorld\n {{\n class Hello\n {{\n static void Main()\n{{\n Console.WriteLine('Hello World!');\n }}\n }}\n }}");
        //                this.listView.Items.Add(new MyItem { Id = 4, Metoda = " static void Main()" });

        //            }
        //            else if (name.Contains("MainWindow.cs"))
        //            {
        //                txtEditor.AppendText($"using System.Windows;\n using System.Windows.Controls;\n namespace IDE\n {{\n public partial class MainWindow : Window\n{{\npublic MainWindow()\n{{\nInitializeComponent();\n}}");
        //                this.listView.Items.Add(new MyItem { Id = 5, Metoda = "MainWindow()" });
        //                this.listView.Items.Add(new MyItem { Id = 6, Metoda = "InitializeComponent()" });
        //            }
        //            else if (name.Contains("Settings.cs"))
        //            {
        //                txtEditor.AppendText($"namespace IDE.Properties {{ \ninternal sealed partial class Settings {{ \npublic Settings() {{ \n}}\n");
        //                this.listView.Items.Add(new MyItem { Id = 7, Metoda = "Settings()" });
        //            }
        //            else if (name.Contains("Main.cpp"))
        //            {
        //                txtEditor.AppendText($"#include <iostream>\n using namespace std;\n int main()\n {{\n cout << 'Hello, World!';\n return 0;\n }});");
        //                this.listView.Items.Add(new MyItem { Id = 3, Metoda = "int main()" });
        //            }
        //        }
        //    }
        //}

        private void MenuItem_Click_DodajDatoteko(object sender, RoutedEventArgs e)
        {
           struktura.Items.Add(new TreeViewItem() { Header = "newItem.txt" });


        }

        private void MenuItem_Click_OdstraniDatoteko(object sender, RoutedEventArgs e)
        {
            if (struktura.SelectedItem == null)
            {
                System.Windows.MessageBox.Show($"There is no selected item!");

            }
            else
                struktura.Items.Remove(uc.UserControlStrukturaProjekta.SelectedItem);
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

