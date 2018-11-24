using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: XmlnsPrefix("http://metro.optkit.com/winfx/xaml/controls", "optkit")]
[assembly: XmlnsPrefix("http://metro.optkit.com/winfx/xaml/shared", "optkitShared")]

[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/shared", "OptKit.Wpf.UI.Behaviours")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/shared", "OptKit.Wpf.UI.Actions")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/shared", "OptKit.Wpf.UI.Converters")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/controls", "OptKit.Wpf.UI")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/controls", "OptKit.Wpf.UI.Controls")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/controls", "OptKit.Wpf.UI.Controls.Dialogs")]

[assembly: XmlnsPrefix("http://metro.optkit.com/winfx/xaml/avalondock", "optkitAvalonDock")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/avalondock", "OptKit.Wpf.UI.AvalonDock")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/avalondock", "OptKit.Wpf.UI.AvalonDock.Controls")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/avalondock", "OptKit.Wpf.UI.AvalonDock.Converters")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/avalondock", "OptKit.Wpf.UI.AvalonDock.Layout")]
[assembly: XmlnsDefinition("http://metro.optkit.com/winfx/xaml/avalondock", "OptKit.Wpf.UI.AvalonDock.Themes")]

[assembly: AssemblyTitle("OptKit.Wpf.UI")]
[assembly: AssemblyCopyright("Copyright © OptKit.Wpf.UI 2011-2018")]
[assembly: AssemblyDescription("A toolkit for creating Metro / Modern UI styled WPF apps.")]
[assembly: AssemblyCompany("OptKit.Wpf.UI")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0.0")]
[assembly: AssemblyProduct("OptKit.Wpf.UI")]