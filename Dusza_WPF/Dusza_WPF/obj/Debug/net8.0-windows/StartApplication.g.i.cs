﻿#pragma checksum "..\..\..\StartApplication.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ED5D8B11CF231AFE42E306DB559000DC39A734CF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Dusza_WPF;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Dusza_WPF {
    
    
    /// <summary>
    /// StartApplication
    /// </summary>
    public partial class StartApplication : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\StartApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblWarning;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\StartApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbValasztahtoProgramok;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\StartApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblProgramMemoria;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\StartApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblProgramMillimag;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\StartApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbValasztahtoGepek;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\StartApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblGepMemoria;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\StartApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblGepMillimag;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\StartApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStart;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Dusza_WPF;component/startapplication.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StartApplication.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lblWarning = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.cbValasztahtoProgramok = ((System.Windows.Controls.ComboBox)(target));
            
            #line 46 "..\..\..\StartApplication.xaml"
            this.cbValasztahtoProgramok.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbValasztahtoProgramok_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lblProgramMemoria = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lblProgramMillimag = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.cbValasztahtoGepek = ((System.Windows.Controls.ComboBox)(target));
            
            #line 91 "..\..\..\StartApplication.xaml"
            this.cbValasztahtoGepek.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbValasztahtoGepek_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblGepMemoria = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblGepMillimag = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnStart = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

