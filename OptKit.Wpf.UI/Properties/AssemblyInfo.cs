using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: XmlnsPrefix("http://metro.optkit.com/winfx/xaml/controls", "optkit")]
[assembly: XmlnsPrefix("http://metro.optkit.com/winfx/xaml/shared", "optkit")]

[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/shared", "OptKit.Wpf.UI.Behaviours")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/shared", "OptKit.Wpf.UI.Actions")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/shared", "OptKit.Wpf.UI.Converters")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/controls", "OptKit.Wpf.UI")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/controls", "OptKit.Wpf.UI.Controls")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/controls", "OptKit.Wpf.UI.Controls.Dialogs")]

[assembly: AssemblyTitle("OptKit.Wpf.UI")]
[assembly: AssemblyCopyright("Copyright © OptKit.Wpf.UI 2011-2018")]
[assembly: AssemblyDescription("A toolkit for creating Metro / Modern UI styled WPF apps.")]
[assembly: AssemblyCompany("OptKit.Wpf.UI")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0.0")]
[assembly: AssemblyProduct("OptKit.Wpf.UI")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0005:Name of PropertyChangedCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0006:Name of CoerceValueCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0007:Name of ValidateValueCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0036:Avoid side effects in CLR accessors.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0041:Set mutable dependency properties using SetCurrentValue.")]