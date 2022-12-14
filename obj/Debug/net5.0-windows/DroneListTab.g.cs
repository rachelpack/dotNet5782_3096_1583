#pragma checksum "..\..\..\DroneListTab.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C80DFCC1BD09B903EA9A28600147C87E058B886C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL;
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


namespace PL {
    
    
    /// <summary>
    /// DroneListTab
    /// </summary>
    public partial class DroneListTab : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\DroneListTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\DroneListTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UpGrid;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\DroneListTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StatusSelector;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\DroneListTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox MaxWeightSelector;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\DroneListTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddDrone;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\DroneListTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseWindow;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\DroneListTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AllTheDrones;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\DroneListTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView DroneListView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/dronelisttab.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DroneListTab.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.UpGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.StatusSelector = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.MaxWeightSelector = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.AddDrone = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\DroneListTab.xaml"
            this.AddDrone.Click += new System.Windows.RoutedEventHandler(this.AddDrone_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CloseWindow = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\DroneListTab.xaml"
            this.CloseWindow.Click += new System.Windows.RoutedEventHandler(this.CloseWindow_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AllTheDrones = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\DroneListTab.xaml"
            this.AllTheDrones.Click += new System.Windows.RoutedEventHandler(this.AllTheDrones_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DroneListView = ((System.Windows.Controls.ListView)(target));
            
            #line 53 "..\..\..\DroneListTab.xaml"
            this.DroneListView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DroneListView_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

