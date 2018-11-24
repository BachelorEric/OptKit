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
using System.Windows;

namespace OptKit.Wpf.UI.AvalonDock.Themes
{
  public abstract class DictionaryTheme : AvalonDockTheme
    {
    #region Constructors

    public DictionaryTheme()
    {
    }

    public DictionaryTheme( ResourceDictionary themeResourceDictionary )
    {
      this.ThemeResourceDictionary = themeResourceDictionary;
    }

    #endregion

    #region Properties

    public ResourceDictionary ThemeResourceDictionary
    {
      get;
      private set;
    }

    #endregion

    #region Overrides

    public override Uri GetResourceUri()
    {
      return null;
    }

    #endregion
  }
}
