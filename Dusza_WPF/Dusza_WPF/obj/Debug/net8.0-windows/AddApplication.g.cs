﻿#pragma checksum "..\..\..\AddApplication.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6E8AA936C2EE5AE7DAC1A36308A1021DD6897BCE"
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
    /// AddApplication
    /// </summary>
    public partial class AddApplication : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 59 "..\..\..\AddApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLetrehoz;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\AddApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNév;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\AddApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbMillimag;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\AddApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbMemoria;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\AddApplication.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbMennyiAktiv;
        
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
            System.Uri resourceLocater = new System.Uri("/Dusza_WPF;component/addapplication.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddApplication.xaml"
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
            this.btnLetrehoz = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.tbNév = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tbMillimag = ((System.Windows.Controls.TextBox)(target));
            
            #line 83 "..\..\..\AddApplication.xaml"
            this.tbMillimag.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbMemoria = ((System.Windows.Controls.TextBox)(target));
            
            #line 93 "..\..\..\AddApplication.xaml"
            this.tbMemoria.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbMennyiAktiv = ((System.Windows.Controls.TextBox)(target));
            
            #line 104 "..\..\..\AddApplication.xaml"
            this.tbMennyiAktiv.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextInput);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

