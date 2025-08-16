// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace JumpListViewer.Controls
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
			ViewModel.AppId = "MSEdge"; // Set the default AppId
		}
	}
}
