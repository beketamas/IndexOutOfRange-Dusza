﻿#pragma checksum "..\..\..\ManageApplications.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "37D7B8CE8037CEC0D2099BEFD7738BACE2F182D6"
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
        
        
        #line 38 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbKlaszterProgramok;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbProgrampeldanyok;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProgramLeallitas;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\ManageApplications.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProgramSzerkeztese;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\ManageApplications.xaml"
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
            this.lbKlaszterProgramok = ((System.Windows.Controls.ListBox)(target));
            
            #line 41 "..\..\..\ManageApplications.xaml"
            this.lbKlaszterProgramok.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbKlaszterProgramok_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbProgrampeldanyok = ((System.Windows.Controls.ListBox)(target));
            
            #line 49 "..\..\..\ManageApplications.xaml"
            this.lbProgrampeldanyok.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbProgrampeldanyok_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnProgramLeallitas = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.btnProgramSzerkeztese = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.btnPeldanyLeallitasa = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

