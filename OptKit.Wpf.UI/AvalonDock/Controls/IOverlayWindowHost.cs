﻿/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Collections.Generic;
using System.Windows;

namespace OptKit.Wpf.UI.AvalonDock.Controls
{
  internal interface IOverlayWindowHost
  {
    #region Properties

    DockingManager Manager
    {
      get;
    }

    #endregion

    #region Methods

    bool HitTest( Point dragPoint );

    IOverlayWindow ShowOverlayWindow( LayoutFloatingWindowControl draggingWindow );

    void HideOverlayWindow();

    IEnumerable<IDropArea> GetDropAreas( LayoutFloatingWindowControl draggingWindow );

    #endregion
  }
}
