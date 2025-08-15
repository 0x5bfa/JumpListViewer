// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Media.Imaging;

namespace JumpListViewer
{
	public abstract class BaseJumpListItem
	{
		public BitmapImage? Icon { get; set; }

		public string? Text { get; set; }

		public bool IsPinned { get; set; }
	}
}
