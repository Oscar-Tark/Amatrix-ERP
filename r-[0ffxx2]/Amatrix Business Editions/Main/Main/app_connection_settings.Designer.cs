﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Main {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class app_connection_settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static app_connection_settings defaultInstance = ((app_connection_settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new app_connection_settings())));
        
        public static app_connection_settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Admin")]
        public string user_name {
            get {
                return ((string)(this["user_name"]));
            }
            set {
                this["user_name"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string password {
            get {
                return ((string)(this["password"]));
            }
            set {
                this["password"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Internal Server")]
        public string Server_addr {
            get {
                return ((string)(this["Server_addr"]));
            }
            set {
                this["Server_addr"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Internal Data-Base")]
        public string Db_name {
            get {
                return ((string)(this["Db_name"]));
            }
            set {
                this["Db_name"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool local {
            get {
                return ((bool)(this["local"]));
            }
            set {
                this["local"] = value;
            }
        }
    }
}
