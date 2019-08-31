using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Security;
using IDE.Model;
using System.Windows.Threading;
using TreeView = System.Windows.Controls.TreeView;

namespace IDE
{
    /// <summary>
    /// Ob kliku na izhod se program zapre.
    /// Ob kliku na odpri projekt moramo zbrati pot do datotek projekta,
    /// Datoteke v poti se nafilajo v levi expander, kjer se nahaja "Solution Explorer".
    /// Ob kliku na Datoteko v Solution Explorerju, se v RichTextBoxu izpiše vsebina datoteke.
    /// Prav tako se v primeru C# kode, v desnem meniju odprejo metode.
    /// Projekt se da tudi zapreti.
    /// Če je odprt projekt, se ob kliku na dodaj datoteko, doda nova datoteka File1.txt
    /// Ob kliku na datoteko in Odstrani_Datoteko, se datoteka odstrani iz projekta.
    /// Ikone so.
    /// V nastavitvah lahko dodajamo Programske jezike, za C++, C in C# so tipi projektov v naprej določeni.
    /// Nato lahko z gumbom ustvarimo nov projekt, C++, C, C# se avtomatsko filtrira za tip projektov.
    /// Nato lahko Shranimo projekt.
    /// Delo s podatki, Databinding, DataTriggerji so definirani.
    /// User Control sem naredil, A mi databindingi ne delajo, zato je zakomentiran, A je vseeno dodan med reference kot WpfControlLibrary1.
    /// Dodan Dispatcher Timer za shranjevanje Settingov.
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        //// Polje datotek v mapi projekta.
        //string[] files = null;
        //Metode met = new Metode();
        //// ShowProjekt in ShowMetode za Expandanje Expanderja v XML.
        //public bool ShowProjekt { get; set; } = false;
        //public bool ShowMetode { get; set; } = false;
        //public DispatcherTimer dispatcherTimer { get; private set; }
        //public int sec = Properties.Settings.Default.sec;
        //public int min = Properties.Settings.Default.min;
        //public int hr = Properties.Settings.Default.hr;
        TreeViewItem tviI = new TreeViewItem() { Header = "Project", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };
        TreeViewItem tviIA = new TreeViewItem() { Header = "class.cs", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };
        TreeViewItem tviIB = new TreeViewItem() { Header = "class.cs2", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch };

        public MainWindow()
        {
            //if (Properties.Settings.Default.shrani == true)
            //{
            //    dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            //    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //    dispatcherTimer.Interval = new TimeSpan(hr, min, sec);
            //    dispatcherTimer.Start();
            //}
            InitializeComponent();
            //this.DataContext = this;
            //expander.DataContext = ShowProjekt;
            //expander2.DataContext = ShowMetode;

        }
        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    Properties.Settings.Default.Save();
        //}

        // Vrne vrstico, kjer se kurzor nahaja.
        //private int LineNumber()
        //{
        //    // Dobi začetek vrstice, kjer se kurzor nahaja.
        //    TextPointer caretLineStart = txtEditor.CaretPosition.GetLineStartPosition(0);
        //    // Dobi prvo vrstico.
        //    TextPointer p = txtEditor.Document.ContentStart.GetLineStartPosition(0);
        //    // Prva vrstica.
        //    int currentLineNumber = 1;

        //    while (true)
        //    {   // Primerjaj, če je p v višji oz. nižji vrstici od kurzorja.
        //        if (caretLineStart.CompareTo(p) < 0)
        //        {
        //            // Ko je p, ker ga inkrementiramo za 1 vrstico, 1 vrstico višji od vrstice kurzorja, dekrementira
        //            // št. vrstice kjer je p in dobi vrstico kurzorja.
        //            currentLineNumber--;
        //            break;
        //        }
        //        int actualCount;
        //        // Preskakuj za eno vrstico od začetka ( p kaže na ContentStart in se inkrementira.).
        //        p = p.GetLineStartPosition(1, out actualCount);
        //        // actualCount vrača razdaljo od trenutne vrstice += vrstice podane v argumentu, v tem primeru 1, razen
        //        // če smo v zadnji vrstici vrne 0.
        //        if (actualCount == 0)
        //        {
        //            break;
        //        }
        //        currentLineNumber++;
        //    }
        //    return currentLineNumber;
        //}

        //private int RowNumber()
        //{
        //    TextPointer caretPosition = txtEditor.CaretPosition;
        //    TextPointer caretLineStart = caretPosition.GetLineStartPosition(0);
        //    int count = caretLineStart.GetOffsetToPosition(caretPosition);
        //    // Count -1, ker gleda od začetka vrstice, ne od prve črke..
        //    if (count != 0)
        //        count--;
        //    return count;
        //}

        //public IEnumerable<TextRange> GetAllWordRanges(FlowDocument document)
        //{
        //    int count = 0;
        //    string pattern = @"[^\W\d](\w|[-']{1,2}(?=\w))*";
        //    TextPointer pointer = document.ContentStart;
        //    while (pointer != null)
        //    {
        //        if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
        //        {
        //            string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
        //            MatchCollection matches = Regex.Matches(textRun, pattern);
        //            TextPointer start;
        //            foreach (Match match in matches)
        //            {
        //                int startIndex = match.Index;

        //                int length = match.Length;
        //                if (count == 0)
        //                {
        //                    start = pointer.GetPositionAtOffset(startIndex);
        //                    count++;
        //                }
        //                else
        //                {
        //                    if (pointer.GetLineStartPosition(count) == null)
        //                        break;
        //                    start = pointer.GetLineStartPosition(count).GetInsertionPosition(LogicalDirection.Backward);
        //                    count++;
        //                }
        //                TextPointer end = start.GetPositionAtOffset(length);
        //                if (end == null)
        //                    break;
        //                TextPointer end1 = start.GetLineStartPosition(1).GetInsertionPosition(LogicalDirection.Backward);

        //                yield return new TextRange(start, end1);
        //            }
        //        }

        //        pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
        //    }
        //}

        //private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    int lineNum = LineNumber();
        //    int rowNum = RowNumber();
        //    lblCursorPosition.Text = "Vrstica: " + lineNum + " Stolpec: " + rowNum;

        //}

        private void Izhod_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void Dodaj_Projekt(object sender, RoutedEventArgs e)
        //{
        //    //string path = @"C:\Users\Jan\Documents\0 IČR\IDE\IDE\";
        //    string dir = "";
        //    string path = "";
        //    //files = Directory.GetFiles(path);
        //    dir = path;
        //    using (var fbd = new FolderBrowserDialog())
        //    {
        //    DialogResult result = fbd.ShowDialog();
            
        //                  if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
        //                {
        //    files = Directory.GetFiles(fbd.SelectedPath);
        //    //files = Directory.GetFiles(path);
        //    dir = fbd.SelectedPath;
        //            }
        //      }
        //    if (files.Length > 0)
        //    {
        //        Hierarhija root = new Hierarhija() { Title = dir };
        //        foreach (string file in files)
        //        {
        //            root.Items.Add(new Hierarhija() { Title = file });
        //        }
        //        strukturaProjekta.Items.Add(root);
        //        ShowProjekt = true;
        //        expander.DataContext = ShowProjekt;
        //    }

        //}

        //private void Shrani_Projekt()
        //{
        //    using (var fbd = new FolderBrowserDialog())
        //    {
        //        DialogResult result = fbd.ShowDialog();

        //        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
        //        {
        //            string dir = fbd.SelectedPath;
        //            string parentPath = System.IO.Directory.GetParent(files[0]).ToString();
        //            foreach (var file in files)
        //            {
        //                string filename = file.Substring(parentPath.Length);
        //                // Izbrise trailing //
        //                filename = filename.Substring(1);
        //                string path = Path.Combine(dir, filename);
        //                File.WriteAllLines(path, File.ReadAllLines(file));
        //            }

        //        }
        //    }
        //}
        //private void Izbriši_Projekt(object sender, RoutedEventArgs e)
        //{
        //    string header = "Brisanje Projekta";
        //    if (!strukturaProjekta.HasItems)
        //    {
        //        System.Windows.MessageBox.Show("Izbran ni noben projekt!", header);
        //        return;
        //    }
        //    MessageBoxButton button = MessageBoxButton.YesNoCancel;
        //    MessageBoxImage icon = MessageBoxImage.Warning;
        //    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ali želite shraniti trenutni projekt, preden se izbriše?",header, button, icon );
        //    switch (messageBoxResult)
        //    {
        //        case MessageBoxResult.Cancel:
        //            break;
        //        case MessageBoxResult.No:
        //            strukturaProjekta.Items.Clear();
        //            files = null;
        //            break;
        //        case MessageBoxResult.Yes:
        //            Shrani_Projekt();
        //            strukturaProjekta.Items.Clear();
        //            files = null;
        //            break;
        //    }
           

        //}



        //private void strukturaProjekta_GotFocus(object sender, RoutedEventArgs e)
        //{
        //}
        //private void Dodaj_Datoteko(object sender, RoutedEventArgs e)
        //{
        //    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-create-a-file-or-folder
        //    if (files == null)
        //    {
        //        System.Windows.MessageBox.Show("Odprtega ni nobenega projekta!", "Dodajanje Datoteke");
        //        return;
        //    }
        //    // Dodajanje datoteke s statično vsebino
        //    string parentPath = System.IO.Directory.GetParent(files[0]).ToString();
        //    string fileName = "File1.txt";
        //    string add = Path.Combine(parentPath, fileName);
        //    Console.WriteLine(add);
        //    if (!System.IO.File.Exists(add))
        //    {
        //        using (System.IO.FileStream fs = System.IO.File.Create(add))
        //        {
        //            for (byte i = 0; i < 100; i++)
        //            {
        //                fs.WriteByte(i);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("File \"{0}\" already exists.", fileName);
        //        return;
        //    }
        //}
        //private void Odstrani_Datoteko(object sender, RoutedEventArgs e)
        //{
        //    // Iz TreeView-a Izbriše izbrano datoteko, update-a files polje z datotekami, ki niso trenutno izbrana datoteka.
        //    // YaY LINQ! :smile:
        //    Hierarhija h = (Hierarhija)strukturaProjekta.SelectedItem;
        //    TreeViewItem tvi = strukturaProjekta
        //               .ItemContainerGenerator
        //               .ContainerFromItemRecursive(strukturaProjekta.SelectedItem);
        //    files = files.Where(str => str != h.Title).ToArray();
        //    strukturaProjekta.Items.Remove(tvi);

        //    string path = @"C:\Users\Jan\Documents\0 IČR\IDE\IDE\";
        //    string dir = "";
        //    dir = path;
        //    //using (var fbd = new FolderBrowserDialog())
        //    //{
        //    //DialogResult result = fbd.ShowDialog();
        //    //
        //    //              if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
        //    //            {
        //    //files = Directory.GetFiles(fbd.SelectedPath);
        //    //              files = Directory.GetFiles(path);
        //    //dir = fbd.SelectedPath;
        //    //        }
        //    //  }
        //    Hierarhija root = new Hierarhija() { Title = dir };
        //    if (files.Length > 0)
        //    {
        //        foreach (string file in files)
        //        {
        //            root.Items.Add(new Hierarhija() { Title = file });
        //        }
        //        Console.WriteLine(root.Items);
        //    }
        //    strukturaProjekta.Items.Clear();
        //    strukturaProjekta.Items.Add(root);
        //    strukturaProjekta.Items.Refresh();

        //}

        

        //private void Odpri_Datoteko(object sender, RoutedEventArgs e)
        //{
        //    System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        //    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        try
        //        {
        //            var sr = new StreamReader(openFileDialog1.FileName);
        //            txtEditor.Document.Blocks.Clear();
        //            txtEditor.Document.Blocks.Add(new Paragraph(new Run(sr.ReadToEnd())));
        //        }
        //        catch (SecurityException ex)
        //        {
        //            System.Windows.MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
        //            $"Details:\n\n{ex.StackTrace}");
        //        }
        //    }
        //}
        //private void Shrani_Datoteko(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
        //    TextRange textRange = new TextRange(
        //    // TextPointer na začetek RichTextBox.
        //    txtEditor.Document.ContentStart,
        //    // TextPointer na konec RichTextBox.
        //    txtEditor.Document.ContentEnd);
        //    if (saveFileDialog.ShowDialog() == true)
        //        File.WriteAllText(saveFileDialog.FileName, textRange.Text);
        //}
        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    Nastavitve o = new Nastavitve();

        //    o.ShowDialog();
        //}

        private void Nov_Projekt(object sender, RoutedEventArgs e)
        {
            IDE.Nov_Projekt nov = new IDE.Nov_Projekt();
            nov.ShowDialog();
        }

        private void MenuItem_Click_UstvariProjekt(object sender, RoutedEventArgs e)
        {

            tviI.Items.Add(tviIA);
            tviI.Items.Add(tviIB);
            strukturaProjekta.Items.Add(tviI);

        }

        private void MenuItem_Click_ZapriProjekt(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                // do smth
            }
            strukturaProjekta.Items.Remove(tviI);
            tviI.Items.Clear();



        }

        private void strukturaProjekta_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            listView.Items.Clear();
            var item = (e.NewValue as TreeViewItem);
            if (item.Items.Count == 0 )
            {
                string name = item.Header.ToString();
                if (name.Contains(".cs"))
                {
                    this.listView.Items.Add(new MyItem { Id = 1, Metoda = "private void strukturaProjekta_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)" });

                }
            }
        }
    }

}

public class MyItem
{
    public int Id { get; set; }

    public string Metoda { get; set; }
}
public static class myExtension
{

//public static TreeViewItem ContainerFromItemRecursive(this ItemContainerGenerator root, object item)
//{
//    var treeViewItem = root.ContainerFromItem(item) as TreeViewItem;
//    if (treeViewItem != null)
//        return treeViewItem;
//    foreach (var subItem in root.Items)
//    {
//        treeViewItem = root.ContainerFromItem(subItem) as TreeViewItem;
//        var search = (TreeViewItem)treeViewItem?.ItemContainerGenerator.ContainerFromItem(item);
//        if (search != null)
//            return search;
//    }
//    return null;
//}

}
public class TreeItem
{
    public string Name;
    public int Level;
    public TreeItem(string name, int level)
    {
        Name = name; Level = level;
    }
}
