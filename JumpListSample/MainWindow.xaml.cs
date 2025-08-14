// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;

namespace JumpListSample
{
	public sealed partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			AppWindow.Resize(new(600, 800));

			ExtendsContentIntoTitleBar = true;
		}
	}
}
