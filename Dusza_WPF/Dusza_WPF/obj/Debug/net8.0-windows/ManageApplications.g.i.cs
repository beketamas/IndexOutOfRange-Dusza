﻿#pragma checksum "..\..\..\ManageApplications.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3834DA7D87AD727BF63E53176CABF9E9D8A37D17"
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
    /// ManageApplications
    /// </summary>
    public partial class ManageApplications : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 50 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblError;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbKlaszterProgramok;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbProgrampeldanyok;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProgramLeallitas;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProgramSzerkeztese;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProgramokSzetosztasa;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPeldanyLeallitasa;
        
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
            System.Uri resourceLocater = new System.Uri("/Dusza_WPF;V1.0.0.0;component/manageapplications.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ManageApplications.xaml"
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
            this.lblError = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lbKlaszterProgramok = ((System.Windows.Controls.ListBox)(target));
            
            #line 60 "..\..\..\ManageApplications.xaml"
            this.lbKlaszterProgramok.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbKlaszterProgramok_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lbProgrampeldanyok = ((System.Windows.Controls.ListBox)(target));
            
            #line 68 "..\..\..\ManageApplications.xaml"
            this.lbProgrampeldanyok.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbProgrampeldanyok_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnProgramLeallitas = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.btnProgramSzerkeztese = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.btnProgramokSzetosztasa = ((System.Windows.Controls.Button)(target));
            
            #line 105 "..\..\..\ManageApplications.xaml"
            this.btnProgramokSzetosztasa.Click += new System.Windows.RoutedEventHandler(this.btnProgramokSzetosztasa_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnPeldanyLeallitasa = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

