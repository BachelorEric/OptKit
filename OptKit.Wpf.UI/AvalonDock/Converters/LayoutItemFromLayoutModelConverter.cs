﻿/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System;
using System.Windows.Data;
using OptKit.Wpf.UI.AvalonDock.Layout;

namespace OptKit.Wpf.UI.AvalonDock.Converters
{
  public class LayoutItemFromLayoutModelConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      var layoutModel = value as LayoutContent;
      if( layoutModel == null )
        return null;
      if( layoutModel.Root == null )
        return null;
      if( layoutModel.Root.Manager == null )
        return null;

      var layoutItemModel = layoutModel.Root.Manager.GetLayoutItemFromModel( layoutModel );
      if( layoutItemModel == null )
        return Binding.DoNothing;

      return layoutItemModel;
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      throw new NotImplementedException();
    }
  }
}
