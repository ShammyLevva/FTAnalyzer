﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTAnalyzer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.3.0.0")]
    public sealed partial class FileHandling : global::System.Configuration.ApplicationSettingsBase {
        
        private static FileHandling defaultInstance = ((FileHandling)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new FileHandling())));
        
        public static FileHandling Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LoadWithFilters {
            get {
                return ((bool)(this["LoadWithFilters"]));
            }
            set {
                this["LoadWithFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool RetryFailedLines {
            get {
                return ((bool)(this["RetryFailedLines"]));
            }
            set {
                this["RetryFailedLines"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ConvertDiacritics {
            get {
                return ((bool)(this["ConvertDiacritics"]));
            }
            set {
                this["ConvertDiacritics"] = value;
            }
        }
    }
}
