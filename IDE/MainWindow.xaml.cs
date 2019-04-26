using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Hierarhija;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;
using System.Security;
using Microsoft.Win32;


namespace IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] files = null;
        Metode met = new Metode();
        public bool ShowProjekt { get; set; } = false;
        public bool ShowMetode { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            expander.DataContext = ShowProjekt;
            expander2.DataContext = ShowMetode;
        }

        // Vrne vrstico, kjer se kurzor nahaja.
        private int LineNumber()
        {
            // Dobi začetek vrstice, kjer se kurzor nahaja.
            TextPointer caretLineStart = txtEditor.CaretPosition.GetLineStartPosition(0);
            // Dobi prvo vrstico.
            TextPointer p = txtEditor.Document.ContentStart.GetLineStartPosition(0);
            // Prva vrstica.
            int currentLineNumber = 1;

            while (true)
            {   // Primerjaj, če je p v višji oz. nižji vrstici od kurzorja.
                if (caretLineStart.CompareTo(p) < 0)
                {
                    // Ko je p, ker ga inkrementiramo za 1 vrstico, 1 vrstico višji od vrstice kurzorja, dekrementira
                    // št. vrstice kjer je p in dobi vrstico kurzorja.
                    currentLineNumber--;
                    break;
                }
                int actualCount;
                // Preskakuj za eno vrstico od začetka ( p kaže na ContentStart in se inkrementira.).
                p = p.GetLineStartPosition(1, out actualCount);
                // actualCount vrača razdaljo od trenutne vrstice += vrstice podane v argumentu, v tem primeru 1, razen
                // če smo v zadnji vrstici vrne 0.
                if (actualCount == 0)
                {
                    break;
                }
                currentLineNumber++;
            }
            return currentLineNumber;
        }

        private int RowNumber()
        {
            TextPointer caretPosition = txtEditor.CaretPosition;
            TextPointer caretLineStart = caretPosition.GetLineStartPosition(0);
            int count = caretLineStart.GetOffsetToPosition(caretPosition);
            // Count -1, ker gleda od začetka vrstice, ne od prve črke..
            if (count != 0)
                count--;
            return count;
        }

        public IEnumerable<TextRange> GetAllWordRanges(FlowDocument document)
        {
            int count = 0;
            string pattern = @"[^\W\d](\w|[-']{1,2}(?=\w))*";
            TextPointer pointer = document.ContentStart;
            while (pointer != null)
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
                    MatchCollection matches = Regex.Matches(textRun, pattern);
                    TextPointer start;
                    foreach (Match match in matches)
                    {
                        int startIndex = match.Index;

                        int length = match.Length;
                        if (count == 0)
                        {
                            start = pointer.GetPositionAtOffset(startIndex);
                            count++;
                        }
                        else
                        {
                            if (pointer.GetLineStartPosition(count) == null)
                                break;
                            start = pointer.GetLineStartPosition(count).GetInsertionPosition(LogicalDirection.Backward);
                            count++;
                        }
                        TextPointer end = start.GetPositionAtOffset(length);
                        if (end == null)
                            break;
                        TextPointer end1 = start.GetLineStartPosition(1).GetInsertionPosition(LogicalDirection.Backward);

                        yield return new TextRange(start, end1);
                    }
                }

                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
            }
        }

        private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int lineNum = LineNumber();
            int rowNum = RowNumber();
            lblCursorPosition.Text = "Vrstica: " + lineNum + " Stolpec: " + rowNum;

        }

        private void Izhod_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dodaj_Projekt(object sender, RoutedEventArgs e)
        {
            string path = @"C:\Users\Jan\Documents\0 IČR\IDE\IDE\";
            string dir = "";
            files = Directory.GetFiles(path);
            dir = path;
            //using (var fbd = new FolderBrowserDialog())
            //{
            //DialogResult result = fbd.ShowDialog();
            //
            //              if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            //            {
            //files = Directory.GetFiles(fbd.SelectedPath);
            //              files = Directory.GetFiles(path);
            //dir = fbd.SelectedPath;
            //        }
            //  }
            if (files.Length > 0)
            {
                Hierarhija.Hierarhija root = new Hierarhija.Hierarhija() { Title = dir };
                foreach (string file in files)
                {
                    root.Items.Add(new Hierarhija.Hierarhija() { Title = file });
                }
                strukturaProjekta.Items.Add(root);
                ShowProjekt = true;
                expander.DataContext = ShowProjekt;
            }

        }

        private void Shrani_Projekt()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string dir = fbd.SelectedPath;
                    string parentPath = System.IO.Directory.GetParent(files[0]).ToString();
                    foreach (var file in files)
                    {
                        string filename = file.Substring(parentPath.Length);
                        // Izbrise trailing //
                        filename = filename.Substring(1);
                        string path = Path.Combine(dir, filename);
                        File.WriteAllLines(path, File.ReadAllLines(file));
                    }

                }
            }
        }
        private void Izbriši_Projekt(object sender, RoutedEventArgs e)
        {
            string header = "Brisanje Projekta";
            if (!strukturaProjekta.HasItems)
            {
                System.Windows.MessageBox.Show("Izbran ni noben projekt!", header);
                return;
            }
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ali želite shraniti trenutni projekt, preden se izbriše?",header, button, icon );
            switch (messageBoxResult)
            {
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.No:
                    strukturaProjekta.Items.Clear();
                    files = null;
                    break;
                case MessageBoxResult.Yes:
                    Shrani_Projekt();
                    strukturaProjekta.Items.Clear();
                    files = null;
                    break;
            }
           

        }



        private void strukturaProjekta_GotFocus(object sender, RoutedEventArgs e)
        {
            if (met.Metode1 != null)
                met.Metode1.Clear();
            TreeViewItem tvi = strukturaProjekta
                       .ItemContainerGenerator
                       .ContainerFromItemRecursive(strukturaProjekta.SelectedItem);
            Hierarhija.Hierarhija h = (Hierarhija.Hierarhija)strukturaProjekta.SelectedItem;
            Regex rx = new Regex(@"\w\.");
            if (h != null && (rx.IsMatch(h.Title)))
            {
                Console.WriteLine(rx.IsMatch(h.Title));
                string text = File.ReadAllText(h.Title);
                txtEditor.Document.Blocks.Clear();
                txtEditor.Document.Blocks.Add(new Paragraph(new Run(text)));
                if (h.Title.EndsWith(".cs"))
                {
                    IEnumerable<TextRange> wordRanges = GetAllWordRanges(txtEditor.Document);
                    foreach (TextRange wordRange in wordRanges)
                    {
                        if (wordRange.Text.Trim().StartsWith("public") || wordRange.Text.Trim().StartsWith("private"))
                        {
                            //if (met.Metode1 != null)
                            if (met.Metode1.Contains(wordRange.Text))
                                continue;
                            met.Title = wordRange.Text;
                            met.Metoda = true;
                            wordRange.Text = wordRange.Text.TrimStart('\t', '\n');
                            met.Metode1.Add(wordRange.Text);
                            Console.WriteLine(wordRange.Text);
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
                        }
                        if (wordRange.Text.Trim().Contains("class"))
                        {
                            met.Metoda = false;
                        }

                    }
                    metode.ItemsSource = met.Metode1;
                    metode.Items.Refresh();
                    ShowMetode = true;
                    expander2.DataContext = ShowMetode;

                }

            }
        }
        private void Dodaj_Datoteko(object sender, RoutedEventArgs e)
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-create-a-file-or-folder
            if (files == null)
            {
                System.Windows.MessageBox.Show("Odprtega ni nobenega projekta!", "Dodajanje Datoteke");
                return;
            }
            // Dodajanje datoteke s statično vsebino
            string parentPath = System.IO.Directory.GetParent(files[0]).ToString();
            string fileName = "File1.txt";
            string add = Path.Combine(parentPath, fileName);
            Console.WriteLine(add);
            if (!System.IO.File.Exists(add))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(add))
                {
                    for (byte i = 0; i < 100; i++)
                    {
                        fs.WriteByte(i);
                    }
                }
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }
        }
        private void Odstrani_Datoteko(object sender, RoutedEventArgs e)
        {
            // Iz TreeView-a Izbriše izbrano datoteko, update-a files polje z datotekami, ki niso trenutno izbrana datoteka.
            // YaY LINQ! :smile:
            Hierarhija.Hierarhija h = (Hierarhija.Hierarhija)strukturaProjekta.SelectedItem;
            TreeViewItem tvi = strukturaProjekta
                       .ItemContainerGenerator
                       .ContainerFromItemRecursive(strukturaProjekta.SelectedItem);
            files = files.Where(str => str != h.Title).ToArray();
            strukturaProjekta.Items.Remove(tvi);

            string path = @"C:\Users\Jan\Documents\0 IČR\IDE\IDE\";
            string dir = "";
            dir = path;
            //using (var fbd = new FolderBrowserDialog())
            //{
            //DialogResult result = fbd.ShowDialog();
            //
            //              if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            //            {
            //files = Directory.GetFiles(fbd.SelectedPath);
            //              files = Directory.GetFiles(path);
            //dir = fbd.SelectedPath;
            //        }
            //  }
            Hierarhija.Hierarhija root = new Hierarhija.Hierarhija() { Title = dir };
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    root.Items.Add(new Hierarhija.Hierarhija() { Title = file });
                }
                Console.WriteLine(root.Items);
            }
            strukturaProjekta.Items.Clear();
            strukturaProjekta.Items.Add(root);
            strukturaProjekta.Items.Refresh();

        }

        

        private void Odpri_Datoteko(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(openFileDialog1.FileName);
                    txtEditor.Document.Blocks.Clear();
                    txtEditor.Document.Blocks.Add(new Paragraph(new Run(sr.ReadToEnd())));
                }
                catch (SecurityException ex)
                {
                    System.Windows.MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
        private void Shrani_Datoteko(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            TextRange textRange = new TextRange(
            // TextPointer na začetek RichTextBox.
            txtEditor.Document.ContentStart,
            // TextPointer na konec RichTextBox.
            txtEditor.Document.ContentEnd);
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, textRange.Text);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Nastavitve o = new Nastavitve();

            o.ShowDialog();
        }

        private void Nov_Projekt(object sender, RoutedEventArgs e)
        {
            IDE.Nov_Projekt nov = new IDE.Nov_Projekt();
            nov.ShowDialog();
        }
    }

}
public class Metode
{
    public List<string> Metode1 { get; set; }
    public string Title { get; set; }
    public bool Metoda { get; set; }
    public Metode()
    {
        this.Metode1 = new List<string>();
    }

}


public static class myExtension
{

public static TreeViewItem ContainerFromItemRecursive(this ItemContainerGenerator root, object item)
{
    var treeViewItem = root.ContainerFromItem(item) as TreeViewItem;
    if (treeViewItem != null)
        return treeViewItem;
    foreach (var subItem in root.Items)
    {
        treeViewItem = root.ContainerFromItem(subItem) as TreeViewItem;
        var search = (TreeViewItem)treeViewItem?.ItemContainerGenerator.ContainerFromItem(item);
        if (search != null)
            return search;
    }
    return null;
}

}
