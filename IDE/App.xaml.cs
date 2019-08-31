using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IDE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (IDE.Properties.Settings.Default.CustomSettings == null)
            {
                IDE.Properties.Settings.Default.CustomSettings = new IDE.Settings();
            }
            if (IDE.Properties.Settings.Default.CustomSettings.programskiJezik.Count == 0)
            {

                IDE.Properties.Settings.Default.CustomSettings.programskiJezik.Add(new ProgramskiJezik("c#", new string[] { "wpf app", "console app" }));
                IDE.Properties.Settings.Default.CustomSettings.programskiJezik.Add(new ProgramskiJezik("c++", new string[] { "console app" }));
                IDE.Properties.Settings.Default.CustomSettings.ogrodjas.Add(new Ogrodje(".Net core WebApi"));
                IDE.Properties.Settings.Default.CustomSettings.ogrodjas.Add(new Ogrodje(".Net framework WebApi"));
            }
        }
    }
}
