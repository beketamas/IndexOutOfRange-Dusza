﻿#pragma checksum "..\..\..\ClusterTracer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D9C7B2F99C023B6CD9D622673C8496C7AD7D54BE"
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
    /// ClusterTracer
    /// </summary>
    public partial class ClusterTracer : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\ClusterTracer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbGepek;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\ClusterTracer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbKlaszuter;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\ClusterTracer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvGepek;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\ClusterTracer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas cKlaszter;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\ClusterTracer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCenter;
        
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
            System.Uri resourceLocater = new System.Uri("/Dusza_WPF;component/clustertracer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ClusterTracer.xaml"
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
            this.lbGepek = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lbKlaszuter = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.lvGepek = ((System.Windows.Controls.ListView)(target));
            
            #line 40 "..\..\..\ClusterTracer.xaml"
            this.lvGepek.DragOver += new System.Windows.DragEventHandler(this.ListView_DragOver);
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\ClusterTracer.xaml"
            this.lvGepek.Drop += new System.Windows.DragEventHandler(this.ListView_Drop);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cKlaszter = ((System.Windows.Controls.Canvas)(target));
            
            #line 47 "..\..\..\ClusterTracer.xaml"
            this.cKlaszter.Drop += new System.Windows.DragEventHandler(this.Canvas_Drop);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCenter = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

