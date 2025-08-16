// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Media.Imaging;

namespace JumpListViewer.Data
{
	public class ApplicationItem
	{
		public BitmapImage? Icon { get; set; }

		public string? Name { get; set; }

		public string? AppUserModelID { get; set; }
	}
}
