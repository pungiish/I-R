﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IDE.Properties
{


    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.8.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
    {

        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }

        }
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        public ObservableCollection<KeyValuePair<int, string>> programskiJezik
        {
            get
            {
                return ((ObservableCollection<KeyValuePair<int, string>>)(this["programskiJezik"]));
            }
            set
            {
                this["programskiJezik"] = value;
            }
        }
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public ObservableCollection<KeyValuePair<string, string>> tipiProjektov
        {
            get
            {
                return ((ObservableCollection<KeyValuePair<string, string>>)(this["tipiProjektov"]));
            }
            set
            {
                this["tipiProjektov"] = value;
            }
        }
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        [global::System.Configuration.DefaultSettingValueAttribute("")]

        public ObservableCollection<string> ogrodja
        {
            get
            {
                return ((ObservableCollection<string>)(this["ogrodja"]));
            }
            set
            {
                this["ogrodja"] = value;
            }
        }
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("true")]

        public bool shrani
        {
            get
            {
                return (bool)(this["shrani"]);
            }
            set
            {
                this["shrani"] = value;
            }
        }
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]

        public int sec
        {
            get
            {
                return (int)(this["sec"]);
            }
            set
            {
                this["sec"] = value;
            }
        }
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]

        public int min
        {
            get
            {
                return (int)(this["min"]);
            }
            set
            {
                this["min"] = value;
            }
        }
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]

        public int hr
        {
            get
            {
                return (int)(this["hr"]);
            }
            set
            {
                this["hr"] = value;
            }
        }
    }
}