﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Console.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Commands_FeatureFlags_Create_ConsolePresenter_en {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Commands_FeatureFlags_Create_ConsolePresenter_en() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Console.Resources.Commands_FeatureFlags_Create_ConsolePresenter_en", typeof(Commands_FeatureFlags_Create_ConsolePresenter_en).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string Feature_Flag___0___created_ {
            get {
                return ResourceManager.GetString("Feature Flag \"{0}\" created.", resourceCulture);
            }
        }
        
        internal static string _0____1__ {
            get {
                return ResourceManager.GetString("{0}: {1}.", resourceCulture);
            }
        }
        
        internal static string Error___0__ {
            get {
                return ResourceManager.GetString("Error: {0}.", resourceCulture);
            }
        }
        
        internal static string Already_exists {
            get {
                return ResourceManager.GetString("Already exists", resourceCulture);
            }
        }
        
        internal static string Max_length_is_100 {
            get {
                return ResourceManager.GetString("Max length is 100", resourceCulture);
            }
        }
    }
}