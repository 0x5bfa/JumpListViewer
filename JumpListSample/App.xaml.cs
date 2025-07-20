// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.GdiPlus;
using Windows.Win32.System.Com;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.Shell.Common;

namespace JumpListSample
{
	public partial class App : Application
	{
		public static Window? MainWindow { get; private set; }

		public static float Dpi { get; private set; }

		public App()
		{
			InitializeComponent();
		}

		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			MainWindow = new MainWindow();
			MainWindow.Activate();

			Dpi = PInvoke.GetDpiForWindow((HWND)WinRT.Interop.WindowNative.GetWindowHandle(MainWindow)) / 96f;
		}
	}
}
