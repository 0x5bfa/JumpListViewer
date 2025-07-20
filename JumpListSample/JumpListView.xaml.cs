// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace JumpListSample
{
	public sealed partial class JumpListView : UserControl
	{
		public JumpListViewModel ViewModel { get; } = new();

		public JumpListView()
		{
			InitializeComponent();
		}

		private void JumpListView_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel.AppId = "Microsoft.Windows.Explorer"; // Set the default AppId
		}
	}
}
